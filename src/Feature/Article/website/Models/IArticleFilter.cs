namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Article.Models;
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = Constants.ArticleFilter.TemplateId)]
    public interface IArticleFilter
    {
        [SitecoreField(Constants.ArticleFilter.FundsFieldId)]
        IEnumerable<IFund> Funds { get; set; }

        [SitecoreField(Constants.ArticleFilter.ContentTypesFieldId)]
        IEnumerable<IPromoType> ContentTypes { get; set; }

        [SitecoreField(Constants.ArticleFilter.FundManagersFieldId)]
        IEnumerable<IAuthor> FundManagers { get; set; }

        [SitecoreField(Constants.ArticleFilter.FundTeamsFieldId)]
        IEnumerable<IFundTeam> FundTeams { get; set; }

        [SitecoreField(Constants.ArticleFilter.TopicsFieldId)]
        IEnumerable<ITopic> Topics { get; set; }
    }
}
