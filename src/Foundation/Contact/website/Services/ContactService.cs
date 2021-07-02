namespace LionTrust.Foundation.Contact.Services
{
    using System;
    using System.Collections.Generic;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Client;
    using Sitecore.XConnect.Client.Configuration;
    using FuseIT.Sitecore.Personalization.Facets;
    using Sitecore.Diagnostics;
    using FuseIT.Sitecore.Personalization.Extensions;
    using System.Linq;
    using LionTrust.Foundation.Contact.Models;

    public class ContactService : IContactService
    {

        /// <summary>
        /// Save relavant Salesforce data into S4SInfo facet
        /// </summary>
        /// <param name="scVisitorId"></param>
        /// <param name="sfDataForSaving"></param>
        public void SaveCustomSFDataIntoS4SInfoFacet(string scVisitorId, ScContactFacetData sfDataForSaving)
        {
            if (string.IsNullOrEmpty(scVisitorId))
            {
                Log.Info("Sitecore visitor id is null or empty. No Salesforce data saved in the current Sitecore contact facet.", this);
                return;
            }

            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                try
                {
                    Guid visitorGuid = new Guid(scVisitorId);
                    if (visitorGuid != Guid.Empty)
                    {
                        IdentifiedContactReference reference = new IdentifiedContactReference(Sitecore.XConnect.Constants.AliasIdentifierSource, visitorGuid.ToString("D"));
                        var expandOptions = new ContactExpandOptions(S4SInfo.DefaultFacetKey);
                        var xdbContact = client.Get<Contact>(reference, expandOptions);

                        if (xdbContact != null)
                        {
                            var s4sInfoFacet = xdbContact.S4SInfo() ?? new S4SInfo();

                            string sfEntityIdfacetFieldKey = string.Empty;
                            string objectPrefix = sfDataForSaving.SalesforceEntityId.Substring(0, 3);

                            if (objectPrefix.Equals(Foundation.Contact.Constants.PrefixSalesforceContact, StringComparison.InvariantCultureIgnoreCase))
                            {
                                sfEntityIdfacetFieldKey = Foundation.Contact.Constants.SFContactIdFacetKey;
                            }
                            else if (objectPrefix.Equals(Foundation.Contact.Constants.PrefixSalesforceLead, StringComparison.InvariantCultureIgnoreCase))
                            {
                                sfEntityIdfacetFieldKey = Foundation.Contact.Constants.SFLeadIdFacetKey;
                            }
                            else
                            {
                                Log.Info(string.Format("Salesforce entity id {0} refers to an unsupported entity type. No Salesforce data saved in the current Sitecore contact facet.", sfDataForSaving.SalesforceEntityId), this);
                                return;
                            }

                            s4sInfoFacet.Fields[Foundation.Contact.Constants.SFFirstNameFacetKey] = sfDataForSaving.FirstName;
                            s4sInfoFacet.Fields[Foundation.Contact.Constants.SFLastNameFacetKey] = sfDataForSaving.LastName;
                            s4sInfoFacet.Fields[sfEntityIdfacetFieldKey] = sfDataForSaving.SalesforceEntityId;
                            s4sInfoFacet.Fields[Foundation.Contact.Constants.SFOrgIdFacetKey] = sfDataForSaving.SalesforceOrgId;
                            s4sInfoFacet.Fields[Foundation.Contact.Constants.SFRandomGuidFacetKey] = sfDataForSaving.RandomGuidFromSalesforceEntity;

                            var sfFundIdString = (sfDataForSaving.SalesforceFundIds != null) ? string.Join(",", sfDataForSaving.SalesforceFundIds) : string.Empty;
                            s4sInfoFacet.Fields[Foundation.Contact.Constants.SFFundIdFacetKey] = sfFundIdString;

                            client.SetFacet(xdbContact, S4SInfo.DefaultFacetKey, s4sInfoFacet);
                            client.Submit();
                            Log.Info(string.Format("Successfully saved following Salesforce data in to S4SInfo facet in Sitecore contact with visitor id : {0}. [{1}-{2} | {3}-{4} | {5}-{6} | {7}-{8} | {9}-{10} | {11}-{12}]",
                                visitorGuid.ToString("D"), Foundation.Contact.Constants.SFFirstNameFacetKey, sfDataForSaving.FirstName, Foundation.Contact.Constants.SFLastNameFacetKey, sfDataForSaving.LastName, sfEntityIdfacetFieldKey, sfDataForSaving.SalesforceEntityId, Foundation.Contact.Constants.SFOrgIdFacetKey, sfDataForSaving.SalesforceOrgId, Foundation.Contact.Constants.SFRandomGuidFacetKey, sfDataForSaving.RandomGuidFromSalesforceEntity, Foundation.Contact.Constants.SFFundIdFacetKey, sfFundIdString), this);
                        }
                        else
                        {
                            Log.Info(string.Format("No Sitecore contact found with visitor id : {0}. No Salesforce data saved in the current Sitecore contact facet.", visitorGuid.ToString("D")), this);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Exception occured when saving Salesforce data into Sitecore contact facet for visitor id: {0}.", scVisitorId), ex, this);
                    return;
                }
            }
        }

        /// <summary>
        /// Save only Salesforce fund ids into S4SInfo facet
        /// </summary>
        /// <param name="scVisitorId"></param>
        /// <param name="sfFundIds"></param>
        public void SaveSFFundIdsIntoS4SInforFacet(string scVisitorId, List<string> sfFundIds)
        {
            if (string.IsNullOrEmpty(scVisitorId))
            {
                Log.Info("Sitecore visitor id is null or empty. No Salesforce fund ids saved in the current Sitecore contact facet.", this);
                return;
            }

            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                try
                {
                    Guid visitorGuid = new Guid(scVisitorId);
                    if (visitorGuid != Guid.Empty)
                    {
                        IdentifiedContactReference reference = new IdentifiedContactReference(Sitecore.XConnect.Constants.AliasIdentifierSource, visitorGuid.ToString("D"));
                        var expandOptions = new ContactExpandOptions(S4SInfo.DefaultFacetKey);
                        var xdbContact = client.Get<Contact>(reference, expandOptions);

                        if (xdbContact != null)
                        {
                            var s4sInfoFacet = xdbContact.S4SInfo() ?? new S4SInfo();

                            var sfFundIdString = (sfFundIds != null) ? string.Join(",", sfFundIds) : string.Empty;
                            s4sInfoFacet.Fields[Foundation.Contact.Constants.SFFundIdFacetKey] = sfFundIdString;

                            client.SetFacet(xdbContact, S4SInfo.DefaultFacetKey, s4sInfoFacet);
                            client.Submit();
                            Log.Info(string.Format("Successfully saved following Salesforce fund ids [{0}] in to S4SInfo facet in Sitecore contact with visitor id : {0}.", visitorGuid.ToString("D"), sfFundIdString), this);
                        }
                        else
                        {
                            Log.Info(string.Format("No Sitecore contact found with visitor id : {0}. No Salesforce fund ids saved in the current Sitecore contact facet.", visitorGuid.ToString("D")), this);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Exception occured when saving Salesforce fund ids into Sitecore contact facet for visitor id: {0}.", scVisitorId), ex, this);
                    return;
                }
            }
        }

        /// <summary>
        /// Get custom Salesforce data saved in the current Sitecore contact's S4SInfo facet
        /// </summary>
        /// <param name="scVisitorId"></param>
        /// <returns>ScContactFacetData</returns>
        public ScContactFacetData GetCurrentSitecoreContactFacetData(string scVisitorId)
        {
            if (string.IsNullOrEmpty(scVisitorId))
            {
                Log.Info("Sitecore visitor id is null or empty. No custom Salesforce data returned from the current Sitecore contact facet.", this);
                return null;
            }

            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                try
                {
                    Guid visitorGuid = new Guid(scVisitorId);
                    if (visitorGuid != Guid.Empty)
                    {
                        IdentifiedContactReference reference = new IdentifiedContactReference(Sitecore.XConnect.Constants.AliasIdentifierSource, visitorGuid.ToString("D"));
                        var expandOptions = new ContactExpandOptions(S4SInfo.DefaultFacetKey);
                        var xdbContact = client.Get<Contact>(reference, expandOptions);

                        if (xdbContact != null)
                        {
                            var s4sInfoFacet = xdbContact.S4SInfo();
                            if (s4sInfoFacet != null)
                            {
                                var firstName = string.Empty;
                                var lastName = string.Empty;
                                var sfRandomGuid = string.Empty;
                                var sfContactId = string.Empty;
                                var sfLeadId = string.Empty;
                                var sfOrgId = string.Empty;
                                var sfFundIds = new List<string>();

                                if (s4sInfoFacet.Fields.ContainsKey(Foundation.Contact.Constants.SFFirstNameFacetKey))
                                {
                                    firstName = s4sInfoFacet.Fields[Foundation.Contact.Constants.SFFirstNameFacetKey];
                                }

                                if (s4sInfoFacet.Fields.ContainsKey(Foundation.Contact.Constants.SFLastNameFacetKey))
                                {
                                    lastName = s4sInfoFacet.Fields[Foundation.Contact.Constants.SFLastNameFacetKey];
                                }

                                if (s4sInfoFacet.Fields.ContainsKey(Foundation.Contact.Constants.SFRandomGuidFacetKey))
                                {
                                    sfRandomGuid = s4sInfoFacet.Fields[Foundation.Contact.Constants.SFRandomGuidFacetKey];
                                }

                                if (s4sInfoFacet.Fields.ContainsKey(Foundation.Contact.Constants.SFContactIdFacetKey))
                                {
                                    sfContactId = s4sInfoFacet.Fields[Foundation.Contact.Constants.SFContactIdFacetKey];
                                }

                                if (s4sInfoFacet.Fields.ContainsKey(Foundation.Contact.Constants.SFLeadIdFacetKey))
                                {
                                    sfLeadId = s4sInfoFacet.Fields[Foundation.Contact.Constants.SFLeadIdFacetKey];
                                }

                                if (s4sInfoFacet.Fields.ContainsKey(Foundation.Contact.Constants.SFOrgIdFacetKey))
                                {
                                    sfOrgId = s4sInfoFacet.Fields[Foundation.Contact.Constants.SFOrgIdFacetKey];
                                }

                                if (s4sInfoFacet.Fields.ContainsKey(Foundation.Contact.Constants.SFFundIdFacetKey))
                                {
                                    var savedSfFundIdString = s4sInfoFacet.Fields[Foundation.Contact.Constants.SFFundIdFacetKey];
                                    if (!string.IsNullOrEmpty(savedSfFundIdString))
                                    {
                                        Log.Info(string.Format("Salesforce fund ids [{0}] retrieved from Sitecore contact with visitor id: {1}.", savedSfFundIdString, visitorGuid.ToString("D")), this);
                                        sfFundIds = savedSfFundIdString.Split(',').ToList();
                                    }
                                    else
                                    {
                                        Log.Info(string.Format("No Salesforce fund ids saved in Sitecore contact with visitor id: {0}.", visitorGuid.ToString("D")), this);
                                    }
                                }
                                else
                                {
                                    Log.Info(string.Format("SalesforceFundIds facet field not found in Sitecore contact with visitor id: {0}.", visitorGuid.ToString("D")), this);
                                }

                                var returnObj = new ScContactFacetData();
                                returnObj.FirstName = firstName;
                                returnObj.LastName = lastName;
                                returnObj.RandomGuidFromSalesforceEntity = sfRandomGuid;
                                returnObj.SalesforceEntityId = (!string.IsNullOrEmpty(sfContactId)) ? sfContactId : sfLeadId;
                                returnObj.SalesforceOrgId = sfOrgId;
                                returnObj.SalesforceFundIds = sfFundIds;
                                return returnObj;
                            }
                            else
                            {
                                Log.Info(string.Format("S4SInfo facet not found in Sitecore contact with visitor id: {0}. No custom Salesforce data returned.", visitorGuid.ToString("D")), this);
                            }
                        }
                        else
                        {
                            Log.Info(string.Format("No Sitecore contact found with visitor id : {0}. No custom Salesforce data returned.", visitorGuid.ToString("D")), this);
                        }
                    }
                }
                catch (XdbExecutionException ex)
                {
                    Log.Error(string.Format("Exception occured when retrieving custom Salesforce data from Sitecore contact facet for visitor id: {0}. No custom Salesforce data returned.", scVisitorId), ex, this);
                }
            }

            return null;
        }
    }
}