namespace LionTrust.Feature.Navigation.Models
{
    using System;

    using Glass.Mapper.Sc.Fields;
    using Glass.Mapper.Sc.Configuration.Attributes;
   
    public interface IPageLink : INavigationGlassBase
    {
        [SitecoreField(Constants.PageLink.Link_FieldID)]
        Link PageLink { get; set; }

        [SitecoreField(Constants.PageLink.LinkGoal_FieldId)]
        Guid PageLinkGoal { get; set; }
    }
}