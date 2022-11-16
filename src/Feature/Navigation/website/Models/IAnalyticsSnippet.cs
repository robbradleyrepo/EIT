namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System;

    public interface IAnalyticsSnippet : INavigationGlassBase
    {
        [SitecoreField(Constants.AnalyticsSnippet.Script_FieldId)]
        string Script { get; set; }
    }
}
