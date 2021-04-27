namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System;

    public interface ICta: INavigationGlassBase
    {
        [SitecoreField(Constants.Cta.PrimaryCtaFieldId)]
        Link PrimaryCta { get; set; }

        [SitecoreField(Constants.Cta.PrimaryCtaGoalFieldId)]
        Guid PrimaryCtaGoal { get; set; }

        [SitecoreField(Constants.Cta.SecondaryCtaFieldId)]
        Link SecondaryCta { get; set; }

        [SitecoreField(Constants.Cta.SecondaryCtaGoalFieldId)]
        Guid SecondaryCtaGoal { get; set; }
    }
}
