namespace LionTrust.Feature.MyPreferences.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Contact.Models;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    public interface IFundAccordionList : IGlassBase
    {
        [SitecoreField(Constants.FundAccordionList.TeamFundsTitle_FieldId)]
        string TeamFundsTitle { get; set; }

        [SitecoreField(Constants.FundAccordionList.FollowTeamTitle_FieldId)]
        string FollowTeamTitle { get; set; }

        IList<SFProcess> SFProcessList { get; set; }
    }
}