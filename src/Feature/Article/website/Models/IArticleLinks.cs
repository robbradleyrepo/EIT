namespace LionTrust.Feature.Article.Models
{
    using System;

    using Glass.Mapper.Sc.Configuration.Attributes;
    

    public interface IArticleLinks : IArticleGlassBase
    {
        [SitecoreField(Constants.ArticleLinks.ShareTooltip_FieldId)]
        string ShareTooltip { get; set; }

        [SitecoreField(Constants.ArticleLinks.ShareLabel_FieldId)]
        string ShareLabel { get; set; }

        [SitecoreField(Constants.ArticleLinks.ShareLinkGoal_FieldId)]
        Guid ShareLinkGoal { get; set; }

        [SitecoreField(Constants.ArticleLinks.DownloadLabel_FieldId)]
        string DownloadLabel { get; set; }

        [SitecoreField(Constants.ArticleLinks.DownloadGoal_FieldId)]
        Guid DownloadGoal { get; set; }
    }
}