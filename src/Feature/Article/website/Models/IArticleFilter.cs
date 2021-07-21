namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = "{B0C943DC-16B9-44A0-B4ED-92D7492B3813}")]
    public interface IArticleFilter
    {
        [SitecoreField(Constants.ArticleFilter.FundsFieldId)]
        IEnumerable<IFund> Funds { get; set; }

        [SitecoreField(Constants.ArticleFilter.ContentTypesFieldId)]
        IEnumerable<IContentType> ContentTypes { get; set; }

        [SitecoreField(Constants.ArticleFilter.FundManagersFieldId)]
        IEnumerable<IAuthor> FundManagers { get; set; }

        [SitecoreField(Constants.ArticleFilter.FundTeamsFieldId)]
        IEnumerable<IFundTeam> FundTeams { get; set; }

        [SitecoreField(Constants.ArticleFilter.TopicsFieldId)]
        IEnumerable<ITopic> Topics { get; set; }
    }
}
