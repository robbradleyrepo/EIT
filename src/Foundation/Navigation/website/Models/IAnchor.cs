namespace LionTrust.Foundation.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using Sitecore.Rules;

    public interface IAnchor: IGlassBase
    {
        [SitecoreField(Constants.Anchor.AnchorFieldId)]
        string AnchorId { get; set; }

        [SitecoreField(Constants.Anchor.AnchorNameFieldId)]
        string AnchorName { get; set; }

        [SitecoreField(Constants.Anchor.AnchorVisibilityRulesFieldId)]
        RuleList<RuleContext> AnchorVisibilityRules { get; set; }
    }
}
