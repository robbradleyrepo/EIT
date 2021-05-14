namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    public interface IRelatedArticles: IGlassBase
    {
        [SitecoreField(Constants.RelatedArticles.HeadingFieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.RelatedArticles.ArticlesFieldId)]
        IEnumerable<IArticle> Articles { get; set; }

        IEnumerable<ILink> Children { get; set; }
    }
}
