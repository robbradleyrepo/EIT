namespace LionTrust.Feature.Article.Models
{
    using System;
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface IArticle : Foundation.Legacy.Models.IArticle
    {
        [SitecoreField(Constants.Article.TopicsFieldId)]
        IEnumerable<ITopic> Topics { get; set; }

        [SitecoreField(Constants.Article.MultipleAuthorsLabel_FieldId)]
        string MultipleAuthorsLabel { get; set; }

        [SitecoreField(Constants.Article.MultipleAuthorsIcon_FieldId)]
        Image MultipleAuthorsIcon { get; set; }

        [SitecoreField(Constants.Article.Date_FieldId)]
        DateTime Date { get; set; }

        [SitecoreField(Constants.Article.Image_FieldId)]
        Image Image { get; set; }
    }
}
