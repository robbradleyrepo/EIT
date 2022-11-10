namespace LionTrust.Foundation.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;
    using System;

    public interface ICtaLink: IGlassBase
    {
        [SitecoreField(Constants.CtaLink.CtaNameFieldId)]
        string CtaName { get; set; }

        [SitecoreField(Constants.CtaLink.CtaLinkFieldId)]
        Link CtaLink { get; set; }

        [SitecoreField(Constants.CtaLink.CtaLinkGoalFieldId)]
        Guid CtaLinkGoal { get; set; }
    }
}
