namespace LionTrust.Foundation.Contact.Repositories
{
    using LionTrust.Foundation.Contact.Models;
    using LionTrust.Foundation.Contact.Services;
    using Sitecore.Diagnostics;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PersonalizedContentPageRepository : IPersonalizedContentPageRepository
    {
        private readonly ISitecoreContactUtility _sitecoreContactUtility;

        public PersonalizedContentPageRepository(ISitecoreContactUtility sitecoreContactUtility) 
        {
            _sitecoreContactUtility = sitecoreContactUtility;
        }

        /// <summary>
        /// Identify Sitecore contact and save relavant Salesforce data in the current Sitecore contact's S4SInfo facet
        /// </summary>
        /// <param name="sfEntityId"></param>
        /// <param name="sfRandomGUID"></param>
        /// <param name="isContact"></param>
        public bool IdentifySitecoreContactAndSaveSFDataInFacet(Context context)
        {
            try
            {
                var sfEntityUtilityObj = new SFEntityUtility();
                var sfEmailPref = context.Preferences;

                if (sfEmailPref != null && !string.IsNullOrEmpty(sfEmailPref.EmailAddress))
                {
                    var scVisitorId = sfEntityUtilityObj.IdentifyVisitorAndGetVisitorId(sfEmailPref.EmailAddress);
                    if (!string.IsNullOrEmpty(scVisitorId))
                    {                                             
                        var sfFundIdList = new List<string>();
                        
                        foreach (var tempSFProcessItem in sfEmailPref.SFProcessList)
                        {
                            if (tempSFProcessItem != null && tempSFProcessItem.SFFundList != null && tempSFProcessItem.SFFundList.Any())
                            {
                                foreach(var tempSFFundItem in tempSFProcessItem.SFFundList)
                                {
                                    if(tempSFFundItem.IsFundSelected)
                                    {
                                        //Get selected fund ids in the 15 character format
                                        sfFundIdList.Add(tempSFFundItem.SFFundId.Substring(0, 15));
                                    }
                                }
                            }
                        }

                        var sfDataObj = new ScContactFacetData();
                        sfDataObj.FirstName = sfEmailPref.FirstName;
                        sfDataObj.LastName = sfEmailPref.LastName;
                        sfDataObj.SalesforceOrgId = sfEmailPref.SFOrgId;
                        sfDataObj.SalesforceFundIds = sfFundIdList;
                        sfDataObj.Unsubscribed = sfEmailPref.Unsubscribe;
                        sfDataObj.TortoiseNewsletter = sfEmailPref.TortoiseNewsletter;

                        //Save relavant Salesforce data in Sitecore contact facet                        
                        _sitecoreContactUtility.SaveCustomSFDataIntoS4SInfoFacet(scVisitorId, sfDataObj);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Exception occured when identifying Sitecore contact and saving relavant Salesforce fund ids in the S4SInfo facet for Sf entity id : {0} and random guid: {1}.", context.SFEntityId, context.SFRandomGUID), ex, this);                
            }

            return false;
        }
    }
}
