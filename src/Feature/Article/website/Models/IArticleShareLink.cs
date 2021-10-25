namespace LionTrust.Feature.Article.Models
{
    using System;

    using Glass.Mapper.Sc.Configuration.Attributes;
    

    public interface IArticleShareLink : IArticleGlassBase
    {
        [SitecoreField(Constants.ArticleShareLink.Tooltip_FieldId)]
        string ShareTooltip { get; set; }

        [SitecoreField(Constants.ArticleShareLink.Label_FieldId)]
        string ShareLabel { get; set; }

        [SitecoreField(Constants.ArticleShareLink.Goal_FieldId)]
        Guid ShareLinkGoal { get; set; }       
    }
}