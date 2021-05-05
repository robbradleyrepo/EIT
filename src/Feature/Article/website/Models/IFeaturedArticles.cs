namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    public interface IFeaturedArticles: IGlassBase
    {
        [SitecoreField(Constants.FeaturedArticles.HeadingFieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.FeaturedArticles.ArticlesFieldId)]
        IEnumerable<IArticle> Articles { get; set; }

        IEnumerable<ILink> Children { get; set; }
    }
}
