namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Navigation.Models;
    using System;

    public interface ILinkWithGoal: IPageLink
    {
        [SitecoreField(Constants.LinkWithGoal.GoalFieldId)]
        Guid Goal { get; set; }
    }
}