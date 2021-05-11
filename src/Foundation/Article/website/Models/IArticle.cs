namespace LionTrust.Foundation.Article.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Article;
    
    public interface IArticle : Foundation.Legacy.Models.IArticle
    {
        [SitecoreField(Constants.Article.TopicsFieldId)]
        IEnumerable<ITopic> Topics { get; set; }

        [SitecoreField(Constants.Article.MultipleAuthorsLabel_FieldId)]
        string MultipleAuthorsLabel { get; set; }

        [SitecoreField(Constants.Article.MultipleAuthorsIcon_FieldId)]
        Image MultipleAuthorsIcon { get; set; }

    }
}
