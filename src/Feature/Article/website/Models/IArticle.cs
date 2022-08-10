namespace LionTrust.Feature.Article.Models
{
    using System;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Legacy.Models;

    public interface IArticle : Foundation.Article.Models.IArticle
    {
        IAuthor Author { get; set; }

        [SitecoreField(Constants.Article.TopicsFieldId)]
        IEnumerable<ITopic> Topics { get; set; }

        [SitecoreField(Constants.Article.MultipleAuthorsSetting_FieldId)]
        IMultipleAuthorsSetting MultipleAuthorsSetting { get; set; }

        [SitecoreField(Constants.Article.Date_FieldId)]
        DateTime Date { get; set; }

        [SitecoreField(Constants.Article.ImageOpacity_FieldId)]
        string ImageOpacity { get; set; }

        [SitecoreField(Constants.Article.PdfDocument_FieldId)]
        File PdfDocument { get; set; }

        [SitecoreField(Constants.Article.ArticleVideoUrl)]
        Link ArticleVideoUrl { get; set; }

        [SitecoreField(Constants.Article.PercentageScrollFieldId)]
        int PercentageScroll { get; set; }

        [SitecoreField(Constants.Article.PercentageGoalFieldId)]
        Guid PercentageGoal { get; set; }
    }
}
