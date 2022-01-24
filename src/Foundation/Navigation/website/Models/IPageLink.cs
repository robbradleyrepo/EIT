namespace LionTrust.Foundation.Navigation.Models
{
    using System;

    using Glass.Mapper.Sc.Fields;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IPageLink : IGlassBase
    {
        [SitecoreField(Constants.PageLink.Link_FieldID)]
        Link PageLink { get; set; }

        [SitecoreField(Constants.PageLink.LinkGoal_FieldId)]
        Guid PageLinkGoal { get; set; }
    }
}