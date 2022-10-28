using FuseIT.S4S.SitecoreSalesforceListBuilder.Models;
using FuseIT.S4S.SitecoreSalesforceListBuilder.Repositories;
using FuseIT.S4S.SitecoreSalesforceListBuilder.SitecoreUtility;
using FuseIT.Sitecore.Salesforce;
using LionTrust.Feature.EXM.Repositories.Interfaces;
using LionTrust.Foundation.Contact.Services;
using Sitecore;
using Sitecore.DependencyInjection;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.ContactLists;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LionTrust.Feature.EXM.Repositories.Implementations
{
    public class CustomSalesforceCampaignRepository : SalesforceCampaignRepository, ISalesforceCampaignRepository
    {
        private const string ListNamePattern = @"^[\w _]+$";
        private const string CreateNewList = "createnewlist";
        private const string UpdateList = "updatelist";

        private readonly ISalesforceCampaign _salesforceCampaign;
        private readonly IDefinitionManager<IContactListDefinition> _definitionManager;

        public CustomSalesforceCampaignRepository(ISalesforceCampaign salesforceCampaign) 
            : this(salesforceCampaign, ServiceLocator.ServiceProvider.GetDefinitionManagerFactory().GetDefinitionManager<IContactListDefinition>())
        { }

        public CustomSalesforceCampaignRepository(ISalesforceCampaign salesforceCampaign, IDefinitionManager<IContactListDefinition> definitionManager)
        {
            _salesforceCampaign = salesforceCampaign;
            _definitionManager = definitionManager;
        }

        public async Task<SalesforceCampaignEntity> ImportCampaignsToSitecore(string id, SalesforceCampaignEntity info)
        {
            var contactListCreated = false;
            var contactListIdStr = string.Empty;
            var sitecoreListsUtility = new SitecoreListsUtility();
            try
            {
                var user = Context.User;
                var selectedConnStringName = info.SelectedConnStringName;
                var selectedListMergeOption = info.SelectedListMergeOption;
                var selectedListId = info.SelectedListId;
                var campaignIdString = info.CampaignIdString;
                var customListName = info.CustomListName;
                var customListDescription = info.CustomListDescription;
                var isSuccess = false;
                var showProcessButton = false;
                var empty = string.Empty;
                var message = string.Empty;

                var cultureInfo = Context.Language?.CultureInfo ?? new CultureInfo("en");

                if (!string.IsNullOrEmpty(campaignIdString))
                {
                    var campaignIdStrings = campaignIdString.Split(',');
                    if (campaignIdStrings.Count() == 0)
                    {
                        return GetSalesforceCampaignEntity(false, false, "No campaigns selected. Please go back and select a campaign.");
                    }
                    else if (campaignIdStrings.Count() > 1)
                    {
                        return GetSalesforceCampaignEntity(false, false, "More than one camapign selected. Please go back and select only one campaign.");
                    }

                    if (string.IsNullOrEmpty(customListName))
                    {
                        return GetSalesforceCampaignEntity(false, true, "Please fill the List Name field.");
                    }

                    if (!new Regex(ListNamePattern).IsMatch(customListName))
                    {
                        return GetSalesforceCampaignEntity(false, true, "List Name should only consist of letters, digits or underscores. Please enter a different list name.");
                    }

                    var campaignMembers = _salesforceCampaign.GetCampaignMembers(selectedConnStringName, campaignIdString);
                    if (campaignMembers != null && campaignMembers.Count() > 0)
                    {
                        if (selectedListMergeOption == CreateNewList)
                        {
                            if (sitecoreListsUtility.IsSitecoreContactListExists(customListName))
                            {
                                return GetSalesforceCampaignEntity(false, true, "This list name already exists. Please enter an unique list name.");
                            }

                            var contactListId = Guid.NewGuid();
                            var contactListDefinition = new ContactListDefinition(contactListId, customListName, cultureInfo, customListName, DateTime.UtcNow, user.Profile.UserName);
                            contactListDefinition.Description = customListDescription;
                            await _definitionManager.SaveAsync(contactListDefinition, true);
                            contactListIdStr = contactListId.ToString();
                            contactListCreated = true;
                        }
                        else if (selectedListMergeOption == UpdateList)
                        {
                            if (!sitecoreListsUtility.IsSitecoreContactListExists(customListName) || string.IsNullOrEmpty(selectedListId))
                            {
                                return GetSalesforceCampaignEntity(false, true, "No existing Sitecore list selected to update. Please select an existing Sitecore list.");
                            }

                            var definition = _definitionManager.Get(new Guid(selectedListId), cultureInfo, false);
                            definition.Description = customListDescription;
                            await _definitionManager.SaveAsync(definition, true);
                            contactListIdStr = selectedListId;
                        }
                        else
                        {
                            return GetSalesforceCampaignEntity(false, true, "Select an option: 'Create New List' or 'Update Existing List'.");
                        }

                        var sfCampaignMembers = await _salesforceCampaign.CreateSitecoreContactsFromSFCampaignMembers(selectedConnStringName, campaignMembers, new Guid(contactListIdStr));
                        if (sfCampaignMembers > 0)
                        {
                            isSuccess = true;
                            if (sfCampaignMembers < campaignMembers.Count())
                            {
                                return GetSalesforceCampaignEntity(false, true, "Error has occured trying to create/update the contacts.");
                            }
                            else
                            {
                                message = string.Format("{0} records added/updated out of {0} Salesforce campaign member record(s).", sfCampaignMembers, campaignMembers.Count());
                                Logging.Info(this, message);
                                return GetSalesforceCampaignEntity(isSuccess, showProcessButton, message);
                            }
                        }
                        else if (contactListCreated)
                        {
                            sitecoreListsUtility.DeleteExistingList(contactListIdStr);
                        }
                    }
                }

                return GetSalesforceCampaignEntity(isSuccess, showProcessButton, "Error has occured trying to create/update the contacts.");
            }
            catch (Exception ex)
            {
                if (contactListCreated)
                {
                    if (!string.IsNullOrEmpty(contactListIdStr))
                    {
                        sitecoreListsUtility.DeleteExistingList(contactListIdStr);
                    }
                }
                Logging.Error(this, "Error occured trying to import the campaign from Salesforce", ex);
                throw ex;
            }
        }

        private SalesforceCampaignEntity GetSalesforceCampaignEntity(bool isSuccess, bool showProcessButton, string message)
        {
            var salesforceCampaignEntity = new SalesforceCampaignEntity
            {
                IsSuccess = isSuccess,
                ShowProcessButton = showProcessButton,
                ReturnMessage = message
            };
            return salesforceCampaignEntity;
        }
    }
}