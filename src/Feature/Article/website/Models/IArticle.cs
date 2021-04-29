namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System.Collections.Generic;

    public interface IArticle: Foundation.Legacy.Models.IArticle
    {
        [SitecoreField(Constants.Article.TopicsFieldId)]
        IEnumerable<ITopic> Topics { get; set; }
    }
}
