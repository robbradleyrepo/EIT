namespace LionTrust.Feature.Article.Models
{
    using System;

    using Glass.Mapper.Sc.Configuration.Attributes;
    

    public interface IArticleLinks : IArticleGlassBase
    {
        [SitecoreField(Constants.ArticleLinks.ArticleSharing_FieldId)]
        IArticleShareLink ArticleSharing { get; set; }

        [SitecoreField(Constants.ArticleLinks.DownloadLabel_FieldId)]
        string DownloadLabel { get; set; }

        [SitecoreField(Constants.ArticleLinks.DownloadGoal_FieldId)]
        Guid DownloadGoal { get; set; }
    }
}