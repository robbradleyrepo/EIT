using LionTrust.Foundation.Onboarding.Helpers;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Pipelines.Request.RequestBegin;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace LionTrust.Foundation.Navigation.Pipelines
{
    public class GatedAccessProcessor : RequestBeginProcessor
    {
        public override void Process(RequestBeginArgs args)
        {
            if (Sitecore.Context.Item == null || Sitecore.Context.Site == null)
            {
                return;
            }

            var fundReference = (LookupField)Sitecore.Context.Item.Fields[Legacy.Constants.FundPage.FundReference_FieldId];

            if(fundReference != null && fundReference.TargetItem != null)
            {
                var countryExclusions = (MultilistField)fundReference.TargetItem.Fields[Legacy.Constants.FundAccess.ExcludedCountires_FieldId];
                
                if(countryExclusions != null && countryExclusions.TargetIDs != null)
                {
                    var countryNames = new List<string>();

                    foreach(var id in countryExclusions.TargetIDs)
                    {
                        var countryItem = Sitecore.Context.Database.GetItem(id);

                        if(countryItem != null)
                        {
                            countryNames.Add(countryItem[Onboarding.Constants.Country.CountryName_FieldId]);
                        }
                    }

                    if (!OnboardingHelper.HasAccess(countryNames))
                    {
                        throw new HttpException((int)HttpStatusCode.Unauthorized, "Country not authorised to access this page.");
                    }
                }
            }
        }
    }
}