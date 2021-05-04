namespace LionTrust.Feature.Article.Models
{
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    public interface IFeaturedArticles: IGlassBase
    {
        string Heading { get; set; }

        IEnumerable<IArticle> Articles { get; set; }

        IEnumerable<ILink> Children { get; set; }
    }
}
