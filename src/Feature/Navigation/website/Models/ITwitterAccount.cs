namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    using System;

    [SitecoreType(TemplateId = Constants.TwitterAccount.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface ITwitterAccount : INavigationGlassBase
    {
        [SitecoreField(Constants.TwitterAccount.TwitterAccountName_FieldId)]
        string TwitterAccountName { get; set; }

        [SitecoreField(Constants.TwitterAccount.TwitterLinkGoal_FieldId)]
        Guid CTAGoal { get; set; }
    }
}