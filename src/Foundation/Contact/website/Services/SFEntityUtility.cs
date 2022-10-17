namespace LionTrust.Foundation.Contact.Services
{
    using FuseIT.S4S.WebToSalesforce;
    using FuseIT.Sitecore.Personalization.Facets;
    using FuseIT.Sitecore.SalesforceConnector;
    using FuseIT.Sitecore.SalesforceConnector.DataSource;
    using FuseIT.Sitecore.SalesforceConnector.Entities;
    using FuseIT.Sitecore.SalesforceConnector.SalesforcePartner;
    using FuseIT.Sitecore.SalesforceConnector.SalesforceServiceWrappers;
    using FuseIT.Sitecore.SalesforceConnector.Services;
    using FuseIT.Sitecore.SalesforceConnector.Soql;
    using LionTrust.Foundation.Contact.Enums;
    using LionTrust.Foundation.Contact.Models;
    using LionTrust.Foundation.Onboarding.Helpers;
    using Sitecore.Diagnostics;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SFEntityUtility : ISFEntityUtility
    {
        private SalesforceSession _salesforceSession = null;
        private SalesforceSession SalesforceSession
        {
            get
            {
                if (_salesforceSession == null)
                {
                    _salesforceSession = S4SSessionSingleton.SessionInstance;
                }
                return _salesforceSession;
            }
            set
            {
                _salesforceSession = value;
            }
        }

        /// <summary>
        /// Get email preferences SF Contacts
        /// </summary>
        /// <param name="sfEntityId"></param>
        /// <param name="sfRandomGUID"></param>
        /// <param name="isContact"></param>
        /// <param name="loadFundsForMultiAssetProcesse">By default this parameter is set to false. Becasue in the UI no multi asset funds will be displayed.</param>
        /// <returns></returns>
        public EmailPreferences GetSFEmailPreferences(Context context, bool loadFundsForMultiAssetProcesse = false)
        {
            try
            {
                var returnObj = (context.IsContact) ? GetEmailPrefViewModelFromSFContact(context) : GetEmailPrefViewModelFromSFLead(context);
                if (returnObj != null)
                {
                    returnObj.SFProcessList = GetSFProcessList(context, loadFundsForMultiAssetProcesse);
                    return returnObj;
                }
                else
                {
                    Log.Debug(string.Format("No Salesforce Processes found for Salesforce Entity Id :{0}.", context.SFEntityId), this);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Exception occured when retrieving Salesforce entities for Id: {0}.", context.SFEntityId), ex, this);
                return null;
            }
        }

        public List<SFProcess> GetSFProcessList(Context context = null, bool loadFundsForMultiAssetProcesse = false)
        {
            var retrunProcessList = new List<SFProcess>();

            //Retrieve Salesforce Process entities
            GenericSalesforceService genericSFService = new GenericSalesforceService(this.SalesforceSession, Constants.SFProductEntityName);
            var soqlQuery = string.Format("SELECT {0}, {1}, {2}, (SELECT {3}, {4}, {5}, {6} FROM {7} where RecordType.Name = '{8}' AND {9}='{10}' AND Exclude_from_Preference_Centre__c != True) FROM {11} WHERE RecordType.Name='{12}' AND {13}='{14}' AND Exclude_from_Preference_Centre__c != True",
                                          Product2.Fields.Id.Name, Product2.Fields.Name.Name, Constants.SFProduct_IsMutiAssetScenarioField, Product2.Fields.Id.Name, Product2.Fields.Name.Name, Constants.SFProduct_IsMutiAssetScenarioField, Constants.SFProduct_IsNonUKDomicile, Constants.SFProcess_RefFieldForFunds,
                                          Constants.SFFundRecordTypeName, Constants.SFProduct_StatusField, Constants.SFProductStatusOpenName, Constants.SFProductEntityName,
                                          Constants.SFProcessRecordTypeName, Constants.SFProduct_StatusField, Constants.SFProductStatusOpenName);

            Log.Debug(string.Format("GetSFEmailPreferences() soqlQuery={0}.", soqlQuery), this);

            List<GenericSalesforceEntity> sfProcessList = genericSFService.GetBySoql(soqlQuery);

            if (sfProcessList != null && sfProcessList.Count > 0)
            {

                List<GenericSalesforceEntity> fundPreferneceList = null;

                if (context != null)
                {
                    var processIdList = sfProcessList.Select(x => x.InternalFields[Product2.Fields.Id.Name]).ToList();
                    fundPreferneceList = GetFundPreferenceEntities(context, processIdList, true);
                }

                foreach (var sfProcess in sfProcessList)
                {
                    //Generate custom process object
                    var customSfProcessObj = new SFProcess();
                    var processId = sfProcess.Id;

                    customSfProcessObj.SFProcessId = processId;
                    customSfProcessObj.SFProcessName = sfProcess.InternalFields[Product2.Fields.Name.Name];

                    var isMultiAssetProcess = sfProcess.InternalFields.GetField<bool>(Constants.SFProduct_IsMutiAssetScenarioField);
                    var fundList = new List<SFFund>();

                    //Get related SF Funds for each SF Process
                    var subQueryFundItemList = sfProcess.InternalFields.GetSubQuery<GenericSalesforceEntity>(Constants.SFProcess_RefFieldForFunds);
                    foreach (var subQueryFundItem in subQueryFundItemList)
                    {
                        var isMultiAssetFund = subQueryFundItem.InternalFields.GetField<bool>(Constants.SFProduct_IsMutiAssetScenarioField);

                        //This IF statement included to handle special scenario for "Multi Asset Process".
                        //For the SF Process called "Multi Asset Process" we only need to consider "Multi Asset Portfolio (Non Platform Specific)" fund.
                        //To identified  "Multi Asset Process" and "Multi Asset Portfolio (Non Platform Specific)", there is a checkbox called "Is Multi Asset Scenario" in SF.
                        if (!isMultiAssetProcess || (isMultiAssetProcess && isMultiAssetFund))
                        {
                            //Generate custom fund object
                            var sfFundObj = new SFFund();
                            sfFundObj.SFFundId = subQueryFundItem.Id;
                            sfFundObj.SFFundName = subQueryFundItem.InternalFields[Product2.Fields.Name.Name];
                            var IsFundSelected = false;

                            if (fundPreferneceList != null && fundPreferneceList.Where(x => x.InternalFields[Constants.SFFundPref_RefFieldForProcessId] == processId && x.InternalFields[Constants.SFFundPref_FundField] == subQueryFundItem.Id).Count() > 0)
                            {
                                IsFundSelected = true;
                            }

                            //Set PrimarySortOrder according to the "NonUKDomicile" checkbox in SF. If "NonUKDomicile" checked in a fund, set the sort order as 1, so that fund will apear in the bottom of the fund list. 
                            sfFundObj.PrimarySortOrder = (subQueryFundItem.InternalFields.GetField<bool>(Constants.SFProduct_IsNonUKDomicile)) ? 1 : 0;

                            sfFundObj.IsFundSelected = IsFundSelected;
                            fundList.Add(sfFundObj);
                        }
                    }

                    var isProcessSelect = false;
                    if ((fundList.Count > 0 && fundList.Where(x => x.IsFundSelected).Count() == fundList.Count))
                    {
                        isProcessSelect = true;
                    }

                    customSfProcessObj.IsProcessSelected = isProcessSelect;
                    //Load funds if the the process is not categorized as MultiAsst or explicitly passed "loadFundsForMultiAssetProcesse" parameter as true.
                    //When displying processes and funds in Email preference UI, fund list need not to be display for multi asset processes. 
                    customSfProcessObj.SFFundList = (!isMultiAssetProcess || loadFundsForMultiAssetProcesse) ? fundList.OrderBy(x => x.PrimarySortOrder).ThenBy(y => y.SFFundName).ToList() : new List<SFFund>();
                    retrunProcessList.Add(customSfProcessObj);
                }

                retrunProcessList = retrunProcessList.OrderBy(x => x.SFProcessName).ToList();
            }
            else
            {
                if (context != null)
                {
                    Log.Debug(string.Format("No Salesforce Processes found for Salesforce Entity Id :{0}.", context.SFEntityId), this);
                }
                else
                {
                    Log.Debug("No Salesforce Processes found", this);

                }
            }

            return retrunProcessList;
        }

        /// <summary>
        /// Save email preferences in Salesforce
        /// </summary>
        /// <param name="emailPreferences"></param>
        /// <returns></returns>
        public bool SaveEmailPreferences(Context context)
        {
            try
            {
                List<GenericSalesforceEntity> sfFundPreferenceItemsForUpsert = new List<GenericSalesforceEntity>();
                var sfEntityType = (context.IsContact) ? Constants.SFContactEntityName : Constants.SfLeadEntityName;
                var sfEntityId = context.SFEntityId;
                GenericSalesforceService genericService = new GenericSalesforceService(this.SalesforceSession, sfEntityType);
                var sfEntity = genericService.GetByEntityId(sfEntityId);

                if (sfEntity == null)
                {
                    Log.Info(string.Format("No Salesforce entity found for Id: {0}.", sfEntityId), this);
                    return false;
                }

                if (ValidateEmailPreferenceQueryString(sfEntity.InternalFields[Constants.SF_GUIDForEmailPref], context))
                {
                    Log.Info(string.Format("Random GUID which sent via query string, mismatched with the one that saved in actual Salesforce Entity with the Id of {0}.", sfEntityId), this);
                    return false;
                }

                sfEntityId = sfEntity.Id;

                //Update fields
                sfEntity.InternalFields.SetField<bool>(Constants.SF_LTNewsField, context.Preferences.IncludeInLTNews);
                sfEntity.InternalFields.SetField<bool>(Constants.SF_EmailOptOutField, context.Preferences.Unsubscribe);
                sfEntity.InternalFields.SetField<DateTime>(Constants.SF_DateOfConcentField, DateTime.Now);

                if (sfEntity.InternalFields.Contains(Constants.SF_TortoiseNewsletter))
                {
                    sfEntity.InternalFields.SetField<bool>(Constants.SF_TortoiseNewsletter, context.Preferences.TortoiseNewsletter);
                }

                var isAnyProcessOrFundSelected = false;
                if (!context.Preferences.Unsubscribe)
                {
                    //If any SF Process selected from the page, tick FactSheet, RelavantBlog, MonthlyCommentries checkboxes in salesforce                
                    foreach (var tempProcess in context.Preferences.SFProcessList)
                    {
                        if (tempProcess.IsProcessSelected)
                        {
                            isAnyProcessOrFundSelected = true;
                            break;
                        }
                        else
                        {
                            if (tempProcess.SFFundList.Where(x => x.IsFundSelected).Count() > 0)
                            {
                                isAnyProcessOrFundSelected = true;
                                break;
                            }
                        }
                    }
                }

                var isCommonCheckboxesChecked = (!isAnyProcessOrFundSelected || context.Preferences.Unsubscribe) ? false : true;

                if (sfEntity.InternalFields.Contains(Constants.SF_InsightsField))
                {
                    sfEntity.InternalFields.SetField<bool>(Constants.SF_InsightsField, isCommonCheckboxesChecked);
                }

                //If user unsubscribed from the preference center, change the record type to unsubscribe
                if (context.Preferences.Unsubscribe)
                {                    
                    var entityRecordtypes = GetSFEntityRecordTypes(sfEntityType);
                    if (entityRecordtypes != null)
                    {
                        var recordTypeValue = (context.IsContact) ? Constants.SFContactRecordType_UnSubscribe : Constants.SFLeadRecordType_UnSubscribe;
                        var recordTypeObj = entityRecordtypes.Where(x => x.Value == recordTypeValue).FirstOrDefault();
                        if (recordTypeObj.Key != null)
                        {
                            sfEntity.InternalFields[Constants.SF_RecordTypeField] = recordTypeObj.Key.ToString();
                        }
                    }
                }

                var visitorId = string.Empty;

                //Identify current visitor by email and save visitor id in SF
                if (!string.IsNullOrEmpty(context.Preferences.EmailAddress))
                {
                    visitorId = IdentifyVisitorAndGetVisitorId(context.Preferences.EmailAddress);
                    if (!string.IsNullOrEmpty(visitorId))
                    {
                        sfEntity.InternalFields[Constants.SF_SitecoreVistorIdField] = visitorId;
                    }
                }

                //Update SF Contact/Lead               
                genericService.UpdateEntity(sfEntity);

                var selectedSFFundIdList = new List<string>();

                //Update FundPreferences SF entities for UK residents only
                var processIdList = context.Preferences.SFProcessList.Select(x => x.SFProcessId).ToList();
                List<GenericSalesforceEntity> fundPreferneceList = GetFundPreferenceEntities(context, processIdList, false);

                //Generate 'IN' clause for SOQL query
                var processIdString = string.Empty;
                foreach (var processId in processIdList)
                {
                    if (!string.IsNullOrEmpty(processIdString))
                    {
                        processIdString = string.Format("{0},'{1}'", processIdString, processId);
                    }
                    else
                    {
                        processIdString = string.Format("'{0}'", processId);
                    }
                }

                //Retrieve Salesforce Process objects
                GenericSalesforceService genericSFService = new GenericSalesforceService(this.SalesforceSession, Constants.SFProductEntityName);

                var soqlQuery = string.Format("SELECT {0}, {1}, {2}, (SELECT {3}, {4}, {5} FROM {6} where RecordType.Name = '{7}' AND {8}='{9}') FROM {10} WHERE RecordType.Name='{11}' AND {12}='{13}' AND {14} IN ({15})",
                                                  Product2.Fields.Id.Name, Product2.Fields.Name.Name, Constants.SFProduct_IsMutiAssetScenarioField, Product2.Fields.Id.Name, Product2.Fields.Name.Name, Constants.SFProduct_IsMutiAssetScenarioField, Constants.SFProcess_RefFieldForFunds,
                                                  Constants.SFFundRecordTypeName, Constants.SFProduct_StatusField, Constants.SFProductStatusOpenName, Constants.SFProductEntityName,
                                                  Constants.SFProcessRecordTypeName, Constants.SFProduct_StatusField, Constants.SFProductStatusOpenName, Product2.Fields.Id.Name, processIdString);

                List<GenericSalesforceEntity> sfProcessListFromAPI = genericSFService.GetBySoql(soqlQuery);

                foreach (var sfProcessItemFromClient in context.Preferences.SFProcessList)
                {
                    var sfProcessItemFromAPI = sfProcessListFromAPI.Where(x => x.Id == sfProcessItemFromClient.SFProcessId).FirstOrDefault();
                    if (sfProcessItemFromAPI != null)
                    {
                        var fundPreferenceItemListForProcess = fundPreferneceList.Where(x => x.InternalFields[Constants.SFFundPref_RefFieldForProcessId] == sfProcessItemFromAPI.Id);
                        //Get related SF Funds for each SF Process
                        var subQueryFundItemList = sfProcessItemFromAPI.InternalFields.GetSubQuery<GenericSalesforceEntity>(Constants.SFProcess_RefFieldForFunds);

                        //This IF block handles the special scenario for "Multi Asset Process".
                        //If the current Process in the loop is a "Multi Asset Process", Generate a custom fund item and add it to the FundList.
                        //This step is required becasue for "Multi Asset Process" we do not give the option for user to select any fund.  
                        if (sfProcessItemFromAPI.InternalFields.GetField<bool>(Constants.SFProduct_IsMutiAssetScenarioField))
                        {
                            var specialMultiAssetFund = subQueryFundItemList.Where(x => x.InternalFields.GetField<bool>(Constants.SFProduct_IsMutiAssetScenarioField)).FirstOrDefault();
                            if (specialMultiAssetFund != null)
                            {
                                var tempFundObj = new SFFund();
                                tempFundObj.SFFundId = specialMultiAssetFund.Id;
                                tempFundObj.IsFundSelected = (context.Preferences.Unsubscribe) ? false : sfProcessItemFromClient.IsProcessSelected;

                                sfProcessItemFromClient.SFFundList.Add(tempFundObj);
                            }
                        }

                        foreach (var sfFundItemFromClient in sfProcessItemFromClient.SFFundList)
                        {
                            var sfFundItemFromAPI = subQueryFundItemList.Where(x => x.Id == sfFundItemFromClient.SFFundId).FirstOrDefault();
                            if (sfFundItemFromAPI != null)
                            {
                                //Get FundPreference items for each Fund
                                var fundPreferenceItemListForFund = fundPreferenceItemListForProcess.Where(x => x.InternalFields[Constants.SFFundPref_FundField] == sfFundItemFromAPI.Id);

                                if (fundPreferenceItemListForFund != null && fundPreferenceItemListForFund.Count() > 0)
                                {
                                    //Update existing FundPreference items
                                    foreach (var funPreferenceItemForFund in fundPreferenceItemListForFund)
                                    {
                                        funPreferenceItemForFund.InternalFields.SetField<bool>(Constants.SFFundPref_InterestedField, sfFundItemFromClient.IsFundSelected);
                                        sfFundPreferenceItemsForUpsert.Add(funPreferenceItemForFund);
                                    }
                                }
                                else
                                {
                                    //Create new FundPreference items
                                    if (sfFundItemFromClient.IsFundSelected)
                                    {
                                        var newFundPreferenceItem = new GenericSalesforceEntity(Constants.SFFundPreferenceEntityName);
                                        if (context.IsContact)
                                        {
                                            newFundPreferenceItem.InternalFields.SetField(Constants.SFFundPref_ContactField, sfEntityId);
                                        }
                                        else
                                        {
                                            newFundPreferenceItem.InternalFields.SetField(Constants.SFFundPref_LeadField, sfEntityId);
                                        }

                                        newFundPreferenceItem.InternalFields.SetField(Constants.SFFundPref_FundField, sfFundItemFromAPI.Id);
                                        newFundPreferenceItem.InternalFields.SetField(Constants.SFFundPref_InterestedField, true);
                                        sfFundPreferenceItemsForUpsert.Add(newFundPreferenceItem);
                                    }
                                }
                            }

                            //Add selected Salesforce fund ids into a seperate list. So this list can use later to update Sitecore contact's facet as the selected Salesfore fund ids
                            if (sfFundItemFromClient.IsFundSelected)
                            {
                                //Save 15 character salesforce id in the list.  
                                selectedSFFundIdList.Add(sfFundItemFromClient.SFFundId.Substring(0, 15));
                            }
                        }
                    }
                }

                if (sfFundPreferenceItemsForUpsert.Count > 0)
                {
                    List<List<GenericSalesforceEntity>> bulkifiedFundPrefEntities = SplitSalesforceGenericEntityList(sfFundPreferenceItemsForUpsert, 200);
                    foreach (List<GenericSalesforceEntity> fundPreferenceEntities in bulkifiedFundPrefEntities)
                    {
                        genericSFService.UpsertEntities(fundPreferenceEntities, Constants.SFFundPref_IdField);
                    }
                }
                else
                {
                    Log.Debug(string.Format("No FundPreference entities found to be insert/update for Entity Id: {0}.", sfEntityId), this);
                }

                if (!string.IsNullOrEmpty(visitorId))
                {
                    //Save XDB contact object in Salesforce
                    SaveXdbContactSFObject(sfEntityId, context.IsContact, visitorId);

                    var sfDataObj = new ScContactFacetData();
                    sfDataObj.FirstName = sfEntity.InternalFields[Constants.SF_FirstNameField];
                    sfDataObj.LastName = sfEntity.InternalFields[Constants.SF_LastNameField];
                    sfDataObj.SalesforceEntityId = sfEntityId;
                    sfDataObj.SalesforceOrgId = this.SalesforceSession.SalesforceOrganizationId;
                    sfDataObj.RandomGuidFromSalesforceEntity = sfEntity.InternalFields[Constants.SF_GUIDForEmailPref];
                    sfDataObj.SalesforceFundIds = selectedSFFundIdList;
                    sfDataObj.Unsubscribed = sfEntity.InternalFields.GetField<bool>(Constants.SF_EmailOptOutField);

                    //Save relavant Salesforce data in Sitecore contact facet
                    //selectedSFFundIdList contains the 15 character ids of the Salesforce funds. Explicitly save the 15 character fund ids in the contact facet.
                    var scContactUtilityObj = new SitecoreContactUtility();
                    scContactUtilityObj.SaveCustomSFDataIntoS4SInfoFacet(visitorId, sfDataObj);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception occured when saving email preferences to Salesforce", ex, this);
                return false;
            }
        }

        /// <summary>
        /// Save non professional investor as a SF Lead
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public RegisterdUserReturnViewModel SaveNonProfUserAsSFLead(NonProfessionalUser nonProfUser, string preferencesUrl, string fundDashboardUrl)
        {
            try
            {
                var returnObj = new RegisterdUserReturnViewModel();
                returnObj.IsUserExists = false;

                LeadService leadService = new LeadService(this.SalesforceSession);
                var sfLead = leadService.GetByEmail(nonProfUser.Email);
                if (sfLead != null)
                {
                    Log.Info(string.Format("Salesforce Lead exists with the email - {0}", nonProfUser.Email), this);
                    //If user exists in SF as a Lead, indicate it in the UI
                    returnObj.IsUserExists = true;
                    return returnObj;
                }

                //Create a new Lead
                Lead newSFLead = new Lead();
                newSFLead.InternalFields[Constants.SF_FirstNameField] = nonProfUser.FirstName;
                newSFLead.InternalFields[Constants.SF_LastNameField] = nonProfUser.LastName;
                newSFLead.InternalFields[Constants.SF_EmailField] = nonProfUser.Email;
                newSFLead.InternalFields[Constants.SFLead_CompanyField] = nonProfUser.Company;
                newSFLead.InternalFields.SetField<bool>(Constants.SF_UKResident, nonProfUser.IsUKResident);
                newSFLead.InternalFields.SetField<bool>(Constants.SF_CreatedViaPreferenceCentreField, true);

                var leadrecordtypes = GetSFEntityRecordTypes(Constants.SfLeadEntityName);
                if (leadrecordtypes != null)
                {
                    var defaultRecordTypeIdObj = leadrecordtypes.Where(x => x.Value == Constants.SFLeadRecordType_Subscribe).FirstOrDefault();
                    if (defaultRecordTypeIdObj.Key != null)
                    {
                        newSFLead.InternalFields[Constants.SF_RecordTypeField] = defaultRecordTypeIdObj.Key.ToString();
                    }
                }

                var visitorId = IdentifyVisitorAndGetVisitorId(nonProfUser.Email);
                if (!string.IsNullOrEmpty(visitorId))
                {
                    newSFLead.InternalFields[Constants.SF_SitecoreVistorIdField] = visitorId;
                }

                var results = leadService.InsertLead(newSFLead);
                if (results.success)
                {
                    var newlyCreatedLead = leadService.GetByEntityId(results.id);
                    if (newlyCreatedLead != null)
                    {
                        var firstName = newlyCreatedLead.InternalFields[Constants.SF_FirstNameField];
                        var lastName = newlyCreatedLead.InternalFields[Constants.SF_LastNameField];
                        var sfEntityId = newlyCreatedLead.Id.ToString();
                        var sfOrgId = this.SalesforceSession.SalesforceOrganizationId;
                        var randomGuid = newlyCreatedLead.InternalFields[Constants.SF_GUIDForEmailPref];
                        var emailAddress = newlyCreatedLead.InternalFields[Constants.SF_EmailField];
                        var unsubscribed = newlyCreatedLead.InternalFields.GetField<bool>(Constants.SF_EmailOptOutField);

                        if (!string.IsNullOrEmpty(visitorId))
                        {
                            //Save XDB contact object in Salesforce
                            SaveXdbContactSFObject(sfEntityId, false, visitorId);

                            var sfDataObj = new ScContactFacetData();
                            sfDataObj.FirstName = firstName;
                            sfDataObj.LastName = lastName;
                            sfDataObj.SalesforceEntityId = sfEntityId;
                            sfDataObj.SalesforceOrgId = sfOrgId;
                            sfDataObj.RandomGuidFromSalesforceEntity = randomGuid;
                            sfDataObj.SalesforceFundIds = new List<string>();
                            sfDataObj.Unsubscribed = unsubscribed;

                            //Save relavant Salesforce data in Sitecore contact facet
                            var scContactUtilityObj = new SitecoreContactUtility();
                            scContactUtilityObj.SaveCustomSFDataIntoS4SInfoFacet(visitorId, sfDataObj);
                        }

                        //Generate the link to edit email pref page
                        var editEmailPrefPagelink = GetEditEmailPrefPageLink(preferencesUrl, randomGuid, sfEntityId);
                        var fundDashboardlink = GetFundDashboardLink(fundDashboardUrl, randomGuid, sfEntityId);

                        returnObj.FullName = GetFullName(newlyCreatedLead.InternalFields);
                        returnObj.EmailAddress = emailAddress;
                        returnObj.EditEmailPrefLink = editEmailPrefPagelink;
                        returnObj.FundDashboardLink = fundDashboardlink;
                        return returnObj;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Save professional investor as a SF Contact
        /// </summary>
        /// <param name="profUser"></param>
        /// <returns></returns>
        public RegisterdUserReturnViewModel SaveProfUserAsSFContact(ProfessionalUser profUser, string preferencesUrl, string fundDashboardUrl)
        {
            try
            {
                var returnObj = new RegisterdUserReturnViewModel();
                returnObj.IsUserExists = false;

                var contactService = new ContactService(this.SalesforceSession);
                var sfContact = contactService.GetByEmail(profUser.Email);
                if (sfContact != null)
                {
                    Log.Info(string.Format("Salesforce Contact exists with the email - {0}", sfContact.Email), this);
                    //If user exists in SF as a Contact, indicate it in the UI
                    returnObj.IsUserExists = true;
                    return returnObj;
                }

                //Create a new Contact
                Contact newSFContact = new Contact();
                newSFContact.InternalFields[Constants.SF_FirstNameField] = profUser.FirstName;
                newSFContact.InternalFields[Constants.SF_LastNameField] = profUser.LastName;
                newSFContact.InternalFields[Constants.SF_EmailField] = profUser.Email;
                newSFContact.InternalFields[Constants.SFContact_CompanyFCAIdField] = profUser.CompanyId;
                newSFContact.InternalFields[Constants.SFContact_CompanyNameField] = profUser.Company;
                newSFContact.InternalFields.SetField<bool>(Constants.SF_UKResident, profUser.IsUKResident);
                newSFContact.InternalFields[Constants.SFContact_OrgNameField] = profUser.Organisation;
                newSFContact.InternalFields.SetField<bool>(Constants.SF_EmailOptOutField, profUser.Unsubscribed);
                newSFContact.InternalFields.SetField<bool>(Constants.SF_CreatedViaPreferenceCentreField, true);

                var contactRecordtypes = GetSFEntityRecordTypes(Constants.SFContactEntityName);
                if (contactRecordtypes != null)
                {
                    var defaultRecordTypeIdObj = contactRecordtypes.Where(x => x.Value == Constants.SFContactRecortType_SitecoreContact).FirstOrDefault();
                    if (defaultRecordTypeIdObj.Key != null)
                    {
                        newSFContact.InternalFields[Constants.SF_RecordTypeField] = defaultRecordTypeIdObj.Key.ToString();
                    }
                }

                var visitorId = IdentifyVisitorAndGetVisitorId(profUser.Email);
                if (!string.IsNullOrEmpty(visitorId))
                {
                    newSFContact.InternalFields[Constants.SF_SitecoreVistorIdField] = visitorId;
                }

                var results = contactService.InsertContact(newSFContact);
                if (results.success)
                {
                    var newlyCreatedContact = contactService.GetByEntityId(results.id);
                    if (newlyCreatedContact != null)
                    {
                        var firstName = newlyCreatedContact.InternalFields[Constants.SF_FirstNameField];
                        var lastName = newlyCreatedContact.InternalFields[Constants.SF_LastNameField];
                        var sfEntityId = newlyCreatedContact.Id.ToString();
                        var sfOrgId = this.SalesforceSession.SalesforceOrganizationId;
                        var randomGuid = newlyCreatedContact.InternalFields[Constants.SF_GUIDForEmailPref];
                        var emailAddress = newlyCreatedContact.InternalFields[Constants.SF_EmailField];

                        if (!string.IsNullOrEmpty(visitorId))
                        {
                            //Save XDB contact object in Salesforce
                            SaveXdbContactSFObject(results.id, true, visitorId);

                            var sfDataObj = new ScContactFacetData();
                            sfDataObj.FirstName = firstName;
                            sfDataObj.LastName = lastName;
                            sfDataObj.SalesforceEntityId = sfEntityId;
                            sfDataObj.SalesforceOrgId = sfOrgId;
                            sfDataObj.RandomGuidFromSalesforceEntity = randomGuid;
                            sfDataObj.SalesforceFundIds = new List<string>();

                            //Save relavant Salesforce data in Sitecore contact facet
                            var scContactUtilityObj = new SitecoreContactUtility();
                            scContactUtilityObj.SaveCustomSFDataIntoS4SInfoFacet(visitorId, sfDataObj);
                        }

                        //Generate the link to edit email pref page
                        var editEmailPrefPagelink = GetEditEmailPrefPageLink(preferencesUrl, randomGuid, sfEntityId);
                        var fundDashboardPagelink = GetFundDashboardLink(fundDashboardUrl, randomGuid, sfEntityId);


                        returnObj.FullName = GetFullName(newlyCreatedContact.InternalFields);
                        returnObj.EmailAddress = emailAddress;
                        returnObj.EditEmailPrefLink = editEmailPrefPagelink;
                        returnObj.FundDashboardLink = fundDashboardPagelink;
                        return returnObj;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Save hard bounced in Salesforce
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool SaveHardBounced(EntityBase entity)
        {
            try
            {
                var entityType = entity is Contact ? Constants.SFContactEntityName : Constants.SfLeadEntityName;
                GenericSalesforceService genericService = new GenericSalesforceService(this.SalesforceSession, entityType);
                var sfEntity = genericService.GetByEntityId(entity.Id);

                sfEntity.InternalFields.SetField<bool>(Constants.SF_Hard_BouncedField, true);

                //Update SF Contact/Lead               
                var result = genericService.UpdateEntity(sfEntity);
                return result.success;
            }
            catch (Exception ex)
            {
                Log.Error("Exception occured when saving hard bounced to Salesforce", ex, this);
                return false;
            }
        }

        /// <summary>
        /// Get details from SF for resending email pref email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isContact"></param>
        /// <returns></returns>
        public ResendEmailPrefUserDetails GetEmailDetailsForResendEmailPrefLink(string email, bool isContact, string preferencesUrl, string fundDashboardUrl)
        {
            try
            {
                var fullName = string.Empty;
                var entityId = string.Empty;
                var randomGuid = string.Empty;

                if (isContact)
                {
                    var contactService = new ContactService(this.SalesforceSession);
                    var sfContact = contactService.GetByEmail(email);
                    if (sfContact != null)
                    {
                        fullName = GetFullName(sfContact.InternalFields);
                        entityId = sfContact.Id;
                        randomGuid = sfContact.InternalFields[Constants.SF_GUIDForEmailPref];
                    }
                }
                else
                {
                    LeadService leadService = new LeadService(this.SalesforceSession);
                    var sfLead = leadService.GetByEmail(email);
                    if (sfLead != null)
                    {
                        fullName = GetFullName(sfLead.InternalFields);
                        entityId = sfLead.Id;
                        randomGuid = sfLead.InternalFields[Constants.SF_GUIDForEmailPref];
                    }
                }

                if (!string.IsNullOrEmpty(entityId) && !string.IsNullOrEmpty(randomGuid))
                {
                    //Generate the link to edit email pref page
                    var editEmailPrefPagelink = GetEditEmailPrefPageLink(preferencesUrl, randomGuid, entityId);
                    var fundDashboardPagelink = GetFundDashboardLink(fundDashboardUrl, randomGuid, entityId);

                    return new ResendEmailPrefUserDetails { FullName = fullName, EditEmailPrefLink = editEmailPrefPagelink, FundDashboardLink = fundDashboardPagelink };
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Exception occured when retrieving entity details for email: {0}", email), ex, this);
            }

            return null;
        }

        /// <summary>
        /// Get country list from SF
        /// </summary>
        /// <param name="isFromContact"></param>
        /// <param name="defaultOptionText"></param>
        /// <returns></returns>
        public SFCountryListViewModel GetCountryListFromSF(bool isFromContact, string defaultOptionText)
        {
            var returnObj = new SFCountryListViewModel();
            var countryList = new List<SFCountryListItem>();

            //Fill the initial item for country dropdown
            var emptyCountryObj = new SFCountryListItem
            {
                Text = defaultOptionText,
                Value = string.Empty
            };

            countryList.Add(emptyCountryObj);

            try
            {
                var sfEntityName = (isFromContact) ? Constants.SFContactEntityName : Constants.SfLeadEntityName;
                var countryFieldName = (isFromContact) ? Constants.SFContact_CountryField : Constants.SFLead_CountryField;
                FieldService fieldService = FieldService.Instance(sfEntityName, this.SalesforceSession);
                List<SalesforceField> salesforceFields = fieldService.FieldsForObject();

                var countryField = salesforceFields.Where(x => x.name == countryFieldName).FirstOrDefault();
                if (countryField != null && countryField.picklistValues != null && countryField.picklistValues.Length > 0)
                {
                    foreach (var picklistItem in countryField.picklistValues)
                    {
                        var tempCountryObj = new SFCountryListItem
                        {
                            Text = picklistItem.label,
                            Value = picklistItem.value
                        };

                        countryList.Add(tempCountryObj);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception occured when retrieving country list from Salesforce Contact/Lead.", ex, this);
            }

            returnObj.CountryList = countryList;
            return returnObj;
        }

        /// <summary>
        /// Identify visitor by email address in Sitecore and get visitor id
        /// </summary>
        /// <param name="emailAddress"></param>
        public string IdentifyVisitorAndGetVisitorId(string emailAddress)
        {
            string visitorid = string.Empty;
            try
            {
                string identifierSource = Sitecore.Configuration.Settings.GetSetting(Constants.IdentifierSourceConfigName, "S4S");

                if (OnboardingHelper.IdentifyAs(identifierSource, emailAddress))
                {
                    //Get current visitor id
                    WebToEntity webToSfEntity = new WebToEntity();
                    visitorid = webToSfEntity.GetCurrentVisitorId();
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Exception occured when trying to identify current visitor and get vistor id in Sitecore by email address: {0}.", emailAddress), ex, this);
            }

            Log.Info(string.Format("Current Sitecore visitor id - {0}.", visitorid ?? string.Empty), this);
            return visitorid;
        }

        /// <summary>
        /// Is exm unsubscribed
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public bool IsExmUnsubscribed(string entityId)
        {
            if (string.IsNullOrWhiteSpace(entityId))
            {
                Log.Info("Entity Id is null or empty. No email preferences id returned from the entity id.", this);
                return true;
            }

            try
            {
                var unsubscribed = true;

                var sfEntityType = IsContact(entityId) ? Constants.SFContactEntityName : Constants.SfLeadEntityName;
                GenericSalesforceService genericService = new GenericSalesforceService(this.SalesforceSession, sfEntityType);
                var sfEntity = genericService.GetByEntityId(entityId);

                if (sfEntity != null)
                {
                    unsubscribed = sfEntity.InternalFields.GetField<bool>(Constants.SF_EmailOptOutField);
                    var insights = sfEntity.InternalFields.Contains(Constants.SF_InsightsField) ? sfEntity.InternalFields.GetField<bool>(Constants.SF_InsightsField) : true;
                    return unsubscribed || !insights;
                }
                
                Log.Info(string.Format("Salesforce Contact or Lead does not exist with the entity id - {0}", entityId), this);

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Is exm unsubscribed or hard bounced
        /// </summary>
        /// <param name="s4sInfo"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsExmUnsubscribedOrHardBounced(S4SInfo s4sInfo, string email)
        {
            if (s4sInfo == null)
            {
                Log.Info(string.Format("S4SInfo facet is null for the email address - {0}", email), this);
                return false;
            }

            try
            {
                var unsubscribed = false;

                s4sInfo.Fields.TryGetValue(Constants.SF_IdField, out var sfEntityId);
                if (IsContact(sfEntityId))
                {
                    var contactService = new ContactService(this.SalesforceSession);

                    var sfContact = contactService.GetByEntityId(sfEntityId);
                    if (sfContact != null)
                    {
                        unsubscribed = sfContact.InternalFields.GetField<bool>(Constants.SF_EmailOptOutField);
                        var hardBounced = sfContact.InternalFields.GetField<bool>(Constants.SF_Hard_BouncedField);
                        var insights = !sfContact.InternalFields.Contains(Constants.SF_InsightsField) || sfContact.InternalFields.GetField<bool>(Constants.SF_InsightsField);

                        return unsubscribed || hardBounced || !insights;
                    }
                }
                else if (IsLead(sfEntityId))
                {
                    var leadService = new LeadService(this.SalesforceSession);
                    var sfLead = leadService.GetByEntityId(sfEntityId);
                    if (sfLead != null)
                    {
                        unsubscribed = sfLead.InternalFields.GetField<bool>(Constants.SF_EmailOptOutField);
                        var hardBounced = sfLead.InternalFields.GetField<bool>(Constants.SF_Hard_BouncedField);
                        var insights = !sfLead.InternalFields.Contains(Constants.SF_InsightsField) || sfLead.InternalFields.GetField<bool>(Constants.SF_InsightsField);

                        return unsubscribed || hardBounced || !insights;
                    }
                }


                Log.Info(string.Format("Salesforce Contact or Lead does not exist with the contact email - {0}", email), this);

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get salesforce entity by entity id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public EntityBase GetEntityByEntityId(string entityId)
        {
            if (string.IsNullOrWhiteSpace(entityId))
            {
                Log.Info("Entity Id is null or empty.", this);
                return null;
            }

            try
            {
                var entityType = IsContact(entityId) ? Constants.SFContactEntityName
                                : IsLead(entityId) ? Constants.SfLeadEntityName
                                : IsUser(entityId) ? Constants.SfUserEntityName : null;
                
                if (entityId != null)
                {
                    var genericService = new GenericSalesforceService(this.SalesforceSession, entityType);
                    var sfEntity = genericService.GetByEntityId(entityId);
                    
                    if (sfEntity != null)
                    {
                        return sfEntity;
                    }
                }

                Log.Info(string.Format("Salesforce Entity does not exist with the entity id - {0}", entityId), this);
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get salesforce entity by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public EntityBase GetEntityByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                Log.Info("Email address is null or empty.", this);
                return null;
            }

            try
            {
                var contactService = new ContactService(this.SalesforceSession);
                var sfContact = contactService.GetByEmail(email);

                if (sfContact != null)
                {
                    return sfContact;
                }

                var leadService = new LeadService(this.SalesforceSession);
                var sfLead = leadService.GetByEmail(email);

                if (sfLead != null)
                {
                    return sfLead;
                }

                Log.Info(string.Format("Salesforce Entity does not exist with the email - {0}", email), this);
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get entity with updated score
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entityType"></param>
        /// <param name="scorePoints"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        public GenericSalesforceEntity GetEntityWithUpdatedScore(string entityId, string entityType, int scorePoints, IEnumerable<ScoreViewModel> scores)
        {
            try
            {
                GenericSalesforceService genericService = new GenericSalesforceService(this.SalesforceSession, entityType);
                var sfEntity = genericService.GetByEntityId(entityId);

                if (sfEntity == null)
                {
                    Log.Info(string.Format("No Salesforce entity found for Id: {0}.", entityId), this);
                    return null;
                }

                sfEntity.InternalFields[Constants.SF_ScoreField] = GetScorePoints(sfEntity, Constants.SF_ScoreField, scorePoints);

                foreach(var score in scores)
                {
                    sfEntity.InternalFields[score.SalesforceFieldId] = GetScorePoints(sfEntity, score.SalesforceFieldId, score.Score);
                }

                return sfEntity;
            }
            catch (Exception ex)
            {
                Log.Error("Exception occured when retrieving entity object from Salesforce Contact/Lead.", ex, this);
                return null;
            }
        }

        /// <summary>
        /// Generates engagement history object
        /// </summary>
        /// <param name="sfEntityId"></param>
        /// <param name="sfCampaignId"></param>
        /// <param name="entityType"></param>
        /// <param name="interactionType"></param>
        /// <param name="contactList"></param>
        /// <param name="email"></param>
        /// <param name="messageId"></param>
        /// <param name="messageLink"></param>
        /// <param name="link"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public GenericSalesforceEntity GenerateEngagementHistory(
            string sfEntityId, 
            string sfCampaignId, 
            EntityType entityType, 
            InteractionType interactionType, 
            string contactList, 
            string email,
            Guid messageId,
            string messageLink, 
            string link, 
            DateTime date,
            bool firstTime = false)
        {
            var entity = new GenericSalesforceEntity(Constants.SfEngagementHistory);
            entity.InternalFields.SetField(Constants.EngagementHistory.SF_CampaignField, sfCampaignId);
            entity.InternalFields.SetField(Constants.EngagementHistory.SF_ContactListField, contactList);
            entity.InternalFields.SetField(Constants.EngagementHistory.SF_EmailField, email);
            entity.InternalFields.SetField(Constants.EngagementHistory.SF_MessageLinkField, messageLink);
            entity.InternalFields.SetField(Constants.EngagementHistory.SF_LinkField, link);
            entity.InternalFields.SetField(Constants.EngagementHistory.SF_SitecoreCampaignIdField, messageId.ToString("D"));
            entity.InternalFields.SetField(Constants.EngagementHistory.SF_DateTimeField, date.ToString("yyyy-MM-ddTHH:mm:ss.000Z"));

            if (entityType == EntityType.Contact)
            {
                entity.InternalFields.SetField(Constants.EngagementHistory.SF_ContactField, sfEntityId);
            }
            else if (entityType == EntityType.Lead)
            {
                entity.InternalFields.SetField(Constants.EngagementHistory.SF_LeadField, sfEntityId);
            }

            switch (interactionType)
            {
                case InteractionType.LinkClicked:
                    entity.InternalFields.SetField(Constants.EngagementHistory.SF_TypeField, Constants.EngagementHistory.EventTypes.TrackedLinkClicked);
                    break;
                case InteractionType.EmailOpen:
                    entity.InternalFields.SetField(Constants.EngagementHistory.SF_TypeField, Constants.EngagementHistory.EventTypes.EmailOpen);
                    entity.InternalFields.SetField(Constants.EngagementHistory.SF_FirstOpenField, firstTime);
                    break;
                case InteractionType.EmailSent:
                    entity.InternalFields.SetField(Constants.EngagementHistory.SF_TypeField, Constants.EngagementHistory.EventTypes.EmailSent);
                    break;
            }

            return entity;
        }

        /// <summary>
        /// Get identifier by entity
        /// </summary>
        /// <param name="sfEntity"></param>
        /// <returns></returns>
        public string GetIdentifier(EntityBase sfEntity)
        {
            try
            {
                if (sfEntity == null)
                {
                    Log.Info(string.Format("Salesforce Entity is null"), this);
                    return null;
                }

                var identifier = $"{SalesforceSession.SalesforceOrganizationId}_{sfEntity.Id}".ToUpper();
                return identifier;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrInsertEntities(List<GenericSalesforceEntity> entities, string entityType, string externalFieldId)
        {
            //Retrieve Salesforce Process objects
            GenericSalesforceService genericSFService = new GenericSalesforceService(this.SalesforceSession, entityType);

            if (entities.Count > 0)
            {
                List<List<GenericSalesforceEntity>> bulkEntities = SplitSalesforceGenericEntityList(entities, 200);
                foreach (List<GenericSalesforceEntity> list in bulkEntities)
                {
                    var result = genericSFService.UpsertEntities(list, externalFieldId);

                    if (result.Any(x => !x.success))
                    {
                        var errors = result.Where(x => !x.success).SelectMany(x => x.errors).Select(x => x.message).Distinct();
                        foreach(var error in errors)
                        {
                            Log.Debug(string.Format("Error when trying to insert/update object in Salesforce: {0}.", error), this);
                        }
                    }
                }
            }
            else
            {
                Log.Debug("No entities found to be insert/update.", this);
            }
        }

        public string GetEditEmailPrefPageLink(string preferencesUrl, string randomGuid, string entityId)
        {
            var queryStringParams = string.Format("{0}={1}_{2}", Constants.QueryStringNames.EmailPreferencefParams.RefQueryStringKey, randomGuid, entityId);
            var editEmailPrefPagelink = string.Format("{0}?{1}", preferencesUrl, queryStringParams);

            return editEmailPrefPagelink;
        }

        public string GetFundDashboardLink(string fundDashboardUrl, string randomGuid, string entityId)
        {
            var queryStringParams = string.Format("{0}={1}_{2}", Constants.QueryStringNames.EmailPreferencefParams.RefQueryStringKey, randomGuid, entityId);
            var fundDashboardlink = string.Format("{0}?{1}", fundDashboardUrl, queryStringParams);

            return fundDashboardlink;
        }

        public string GetFullName(InternalFields fields)
        {
            return string.Format("{0} {1}", fields[Constants.SF_FirstNameField], fields[Constants.SF_LastNameField]);
        }

        /// <summary>
        /// Get contacts and leads that have not received the welcome email
        /// </summary>
        /// <returns></returns>
        public List<GenericSalesforceEntity> GetEntitiesToSendWecomeEmail(DateTime fromDate)
        {
            var returnList = new List<GenericSalesforceEntity>();

           var sfContactList = GetEntitiesToSendWecomeEmail(fromDate, Constants.SFContactEntityName);
            var sfLeadList = GetEntitiesToSendWecomeEmail(fromDate, Constants.SfLeadEntityName);

            returnList.AddRange(sfContactList);
            returnList.AddRange(sfLeadList);

            return returnList;
        }

        /// <summary>
        /// Get Salesforce Contact/Lead id
        /// </summary>
        /// <param name="s4sInfo"></param>
        /// <returns></returns>
        public string GetSalesforceEntityId(S4SInfo s4sInfo)
        {
            if (s4sInfo.Fields.ContainsKey(Constants.SF_IdField)) 
            {
                return s4sInfo.Fields[Constants.SF_IdField];
            }
            
            if (s4sInfo.Fields.ContainsKey(Constants.SFContactIdFacetKey))
            {
                return s4sInfo.Fields[Constants.SFContactIdFacetKey];
            }

            if (
                s4sInfo.Fields.ContainsKey(Constants.SFLeadIdFacetKey))
            {
                return s4sInfo.Fields[Constants.SFLeadIdFacetKey];
            }

            return null;
        }

        private List<GenericSalesforceEntity> GetEntitiesToSendWecomeEmail(DateTime fromDate, string entityName)
        {
            var genericSFService = new GenericSalesforceService(this.SalesforceSession, entityName);
            var soqlQuery = string.Format("SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6} FROM {7} WHERE IsDeleted = false AND {8} = {9} AND {10} >= {11}",
                                          Constants.SF_IdField, Constants.SF_FirstNameField, Constants.SF_LastNameField, Constants.SF_EmailField,
                                          Constants.SF_UKResident, Constants.SF_GUIDForEmailPref, Constants.SF_CreatedDateField, entityName,
                                          Constants.SF_CreatedViaPreferenceCentreField, false, Constants.SF_CreatedDateField, fromDate.ToString("yyyy-MM-ddTHH:mm:ssZ"));

            Log.Debug(string.Format("Get{0}sToSendWelcomeEmail() soqlQuery={1}.", entityName, soqlQuery), this);

            var sfEntityList = genericSFService.GetBySoql(soqlQuery);
            return sfEntityList;
        }

        /// <summary>
        /// Retrieve record types from the SF entity
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        private Dictionary<Id, string> GetSFEntityRecordTypes(string entityType)
        {
            try
            {
                var describeSObjectResult = this.SalesforceSession.Binding.describeSObjectCached(entityType);
                if (describeSObjectResult != null && describeSObjectResult.recordTypeInfos != null)
                {
                    Dictionary<Id, string> result = new Dictionary<Id, string>();
                    foreach (RecordTypeInfo rti in describeSObjectResult.recordTypeInfos)
                    {
                        result.Add(new Id(rti.recordTypeId), rti.name);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Exception occured when trying to retrieve record types for SF : {0}.", entityType), ex, this);
            }

            return null;
        }

        /// <summary>
        /// Generate email pref view model with SF Contact data
        /// </summary>
        /// <param name="sfEntityId"></param>
        /// <param name="sfRandomGUID"></param>
        /// <returns></returns>
        private EmailPreferences GetEmailPrefViewModelFromSFContact(Context context)
        {
            var sfContact = GetSFContactByIdAndRandomGuid(context);
            if (sfContact != null)
            {
                var returnObj = new EmailPreferences();
                returnObj.SFOrgId = this.SalesforceSession.SalesforceOrganizationId;
                returnObj.EmailAddress = sfContact.Email;
                returnObj.FirstName = sfContact.FirstName;
                returnObj.LastName = sfContact.LastName;
                returnObj.IncludeInLTNews = sfContact.InternalFields.GetField<bool>(Constants.SF_LTNewsField);
                returnObj.Unsubscribe = sfContact.InternalFields.GetField<bool>(Constants.SF_EmailOptOutField);
                returnObj.IsUkResident = sfContact.InternalFields.GetField<bool>(Constants.SF_UKResident);
                returnObj.IsConsentGivenDateEmpty = (sfContact.InternalFields[Constants.SF_DateOfConcentField] == null) ? true : false;
                returnObj.TortoiseNewsletter = sfContact.InternalFields.Contains(Constants.SF_TortoiseNewsletter) ? sfContact.InternalFields.GetField<bool>(Constants.SF_TortoiseNewsletter) : false;

                return returnObj;
            }

            return null;

        }

        /// <summary>
        /// Generate email pref view model with SF Lead data
        /// </summary>
        /// <param name="sfEntityId"></param>
        /// <param name="sfRandomGUID"></param>
        /// <returns></returns>
        private EmailPreferences GetEmailPrefViewModelFromSFLead(Context context)
        {
            var sfLead = GetSFLeadByIdAndRandomGuid(context);
            if (sfLead != null)
            {
                var returnObj = new EmailPreferences();
                returnObj.SFOrgId = this.SalesforceSession.SalesforceOrganizationId;
                returnObj.EmailAddress = sfLead.Email;
                returnObj.FirstName = sfLead.FirstName;
                returnObj.LastName = sfLead.LastName;
                returnObj.IncludeInLTNews = sfLead.InternalFields.GetField<bool>(Constants.SF_LTNewsField);
                returnObj.Unsubscribe = sfLead.InternalFields.GetField<bool>(Constants.SF_EmailOptOutField);
                returnObj.TortoiseNewsletter = sfLead.InternalFields.Contains(Constants.SF_TortoiseNewsletter) ? sfLead.InternalFields.GetField<bool>(Constants.SF_TortoiseNewsletter) : false;
                returnObj.IsUkResident = sfLead.InternalFields.GetField<bool>(Constants.SF_UKResident);
                returnObj.IsConsentGivenDateEmpty = (sfLead.InternalFields[Constants.SF_DateOfConcentField] == null) ? true : false;
                return returnObj;
            }

            return null;
        }

        /// <summary>
        /// Get Salesforce contact by entity id and validate the entity's random guid field against random guid which passed to the method
        /// Ideally this method validates the entity id and random guid which received as query string parameters in edit-email-preferences and my-content pages
        /// </summary>
        /// <param name="sfEntityId"></param>
        /// <param name="sfRandomGUID"></param>
        /// <returns></returns>
        private Contact GetSFContactByIdAndRandomGuid(Context context)
        {
            Log.Debug(string.Format("Trying to get Salesforce Contact from Salesforce Contact Id: {0} and Random Guid: {1}", context.SFEntityId, context.SFRandomGUID), this);

            var contactService = new ContactService(this.SalesforceSession);
            var sfContact = contactService.GetById(context.SFEntityId);

            if (sfContact == null)
            {
                Log.Debug(string.Format("No Salesforce Contact found for Id: {0}.", context.SFEntityId), this);
                return null;
            }

            if (!string.IsNullOrEmpty(sfContact.InternalFields[Constants.SF_GUIDForEmailPref]) && sfContact.InternalFields[Constants.SF_GUIDForEmailPref] == context.SFRandomGUID)
            {
                if (string.IsNullOrEmpty(sfContact.Email))
                {
                    Log.Debug(string.Format("No email address found in Salesforce Contact: {0}.", context.SFEntityId), this);
                    return null;
                }

                Log.Debug("Successfully retrieved Salesforce Contact.", this);
                return sfContact;
            }
            else
            {
                Log.Debug(string.Format("Random GUID which sent via query string, mismatched with the one that saved in actual Salesforce Contact with the Id of {0}.", context.SFEntityId), this);
                return null;
            }
        }

        /// <summary>
        /// Get Salesforce lead by entity id and validate the entity's random guid field against random guid which passed to the method
        /// Ideally this method validates the entity id and random guid which received as query string parameters in edit-email-preferences and my-content pages
        /// </summary>
        /// <param name="sfEntityId"></param>
        /// <param name="sfRandomGUID"></param>
        /// <returns></returns>
        private Lead GetSFLeadByIdAndRandomGuid(Context context)
        {
            Log.Debug(string.Format("Trying to get Salesforce Lead from Salesforce Lead Id: {0} and Random Guid: {1}", context.SFEntityId, context.SFRandomGUID), this);

            LeadService leadService = new LeadService(this.SalesforceSession);
            var sfLead = leadService.GetById(context.SFEntityId);

            if (sfLead == null)
            {
                Log.Debug(string.Format("No Salesforce Lead found for Id: {0}.", context.SFEntityId), this);
                return null;
            }

            if (sfLead.IsConverted ?? false)
            {
                Log.Debug(string.Format("Salesforce Lead for Id {0} has been converted. Converted Leads not supported.", context.SFEntityId), this);
                return null;
            }

            if (!string.IsNullOrEmpty(sfLead.InternalFields[Constants.SF_GUIDForEmailPref]) && sfLead.InternalFields[Constants.SF_GUIDForEmailPref] == context.SFRandomGUID)
            {
                if (string.IsNullOrEmpty(sfLead.Email))
                {
                    Log.Debug(string.Format("No email address found in Salesforce Lead: {0}.", context.SFEntityId), this);
                    return null;
                }

                Log.Debug("Successfully retrieved Salesforce Lead.", this);
                return sfLead;
            }
            else
            {
                Log.Debug(string.Format("Random GUID which sent via query string, mismatched with the one that saved in actual Salesforce Lead with the Id of {0}.", context.SFEntityId), this);
                return null;
            }
        }

        /// <summary>
        /// Get Fund Preferences via S4S
        /// </summary>
        /// <param name="sfContactId">Salesforce Contact Id</param>
        /// <param name="prcessIdList">Salesforce Process Id list</param>
        /// <param name="includeInterestedFilter"></param>
        /// <param name="isContact"></param>
        /// <returns></returns>
        private List<GenericSalesforceEntity> GetFundPreferenceEntities(Context context, List<string> prcessIdList, bool includeInterestedFilter)
        {
            //Retrieve SF Fund Preferences 
            GenericSalesforceEntityDataSource fundPreferenceDataSource = new GenericSalesforceEntityDataSource(Constants.SFFundPreferenceEntityName, this.SalesforceSession);
            var entityTypeFiedName = string.Empty;

            if (context.IsContact)
            {
                entityTypeFiedName = Constants.SFFundPref_ContactField;
            }
            else
            {
                entityTypeFiedName = Constants.SFFundPref_LeadField;
            }

            fundPreferenceDataSource.AddDataSourceFilter(entityTypeFiedName, ComparisonOperator.Equals, context.SFEntityId);
            fundPreferenceDataSource.AddDataSourceFilter(Constants.SFFundPref_FundStatusField, ComparisonOperator.Equals, Constants.SFProductStatusOpenName);
            fundPreferenceDataSource.AddDataSourceFilter(Constants.SFFundPref_ProcessStatusField, ComparisonOperator.Equals, Constants.SFProductStatusOpenName);
            fundPreferenceDataSource.AddDataSourceFilter(Constants.SFFundPref_RefFieldForProcessId, Operator.OperatorFor(ComparisonOperator.In), prcessIdList.ToArray());

            if (includeInterestedFilter)
            {
                fundPreferenceDataSource.AddDataSourceFilter(Constants.SFFundPref_InterestedField, ComparisonOperator.Equals, true);
            }

            string[] fundPreferenceFields = { Constants.SFFundPref_IdField, Constants.SFFundPref_ContactField, Constants.SFFundPref_LeadField, Constants.SFFundPref_FundField, Constants.SFFundPref_FundStatusField,
                                              Constants.SFFundPref_RefFieldForProcessId, Constants.SFFundPref_ProcessStatusField, Constants.SFFundPref_InterestedField };
            return fundPreferenceDataSource.GetQueryResultsAsEntities(fundPreferenceFields);
        }

        /// <summary>
        /// Split SF entity list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private List<List<GenericSalesforceEntity>> SplitSalesforceGenericEntityList(List<GenericSalesforceEntity> list, int size)
        {
            List<List<GenericSalesforceEntity>> splitedResult = new List<List<GenericSalesforceEntity>>();

            for (int i = 0; i < list.Count; i += size)
            {
                splitedResult.Add(list.GetRange(i, Math.Min(size, list.Count - i)));
            }

            return splitedResult;
        }

        /// <summary>
        /// Save XdbContact Salesforce object
        /// </summary>
        /// <param name="sfEntityId"></param>
        /// <param name="isSfContact"></param>
        /// <param name="scVisitorId"></param>
        private void SaveXdbContactSFObject(string sfEntityId, bool isSfContact, string scVisitorId)
        {
            try
            {
                WebToEntity webToSfEntity = new WebToEntity();
                GenericSalesforceService genericService = new GenericSalesforceService(this.SalesforceSession, Constants.SFSitecoreXDBContactObjectName);
                List<GenericSalesforceEntity> matchedEntities = genericService.GetByFieldEquals(Constants.SFSitecoreXDBContactObject_SitecoreAliasIdField, scVisitorId);

                if (matchedEntities == null || matchedEntities.Count == 0)
                {
                    Log.Info($"{nameof(SaveXdbContactSFObject)}(): No {Constants.SFSitecoreXDBContactObjectName} object found for visitor id {scVisitorId}", this);

                    string relatedToField = (isSfContact) ? Constants.SFSitecoreXDBContactObject_ContactField : Constants.SFSitecoreXDBContactObject_LeadField;
                    FieldService fieldService = FieldService.Instance(Constants.SFSitecoreXDBContactObjectName, this.SalesforceSession);

                    GenericSalesforceEntity entityToInsert = new GenericSalesforceEntity(Constants.SFSitecoreXDBContactObjectName);
                    entityToInsert.InternalFields[relatedToField] = sfEntityId;

                    if (fieldService.HasField(Constants.SFSitecoreXDBContactObject_SitecoreAliasIdField))
                    {
                        Guid visitorGuid;

                        if (Guid.TryParse(scVisitorId, out visitorGuid) && visitorGuid != Guid.Empty)
                        {
                            entityToInsert.InternalFields[Constants.SFSitecoreXDBContactObject_SitecoreAliasIdField] = scVisitorId;
                        }
                    }

                    SaveResult saveResult = genericService.Insert(entityToInsert);
                }
                else if (matchedEntities.Count > 0)
                {
                    Log.Info($"{nameof(SaveXdbContactSFObject)}(): {Constants.SFSitecoreXDBContactObjectName} object found for visitor id {scVisitorId}.", this);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(SaveXdbContactSFObject)}(): Error saving {Constants.SFSitecoreXDBContactObjectName} object to Salesforce for visitor id {scVisitorId}.", ex, this);
            }
        }

        private bool ValidateEmailPreferenceQueryString(string queryString, Context context)
        {
            return string.IsNullOrEmpty(queryString) || queryString != context.SFRandomGUID;
        }

        public bool IsContact(string entityId)
        {
            return entityId.StartsWith(Constants.PrefixSalesforceContact, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsLead(string entityId)
        {
            return entityId.StartsWith(Constants.PrefixSalesforceLead, StringComparison.InvariantCultureIgnoreCase);
        }

        private bool IsUser(string entityId)
        {
            return entityId.StartsWith(Constants.PrefixSalesforceUser, StringComparison.InvariantCultureIgnoreCase);
        }

        private string GetScorePoints(GenericSalesforceEntity sfEntity, string fieldName, int scorePoints)
        {
            if (double.TryParse(sfEntity.InternalFields[fieldName], out var score))
            {
                return (score + scorePoints).ToString();
            }

            return scorePoints.ToString();
        }
    }
}
