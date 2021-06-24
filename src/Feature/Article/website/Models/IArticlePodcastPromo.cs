namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = Constants.ArticlePodcastPromo.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface IArticlePodcastPromo : IArticleGlassBase
    {
        [SitecoreField(Constants.ArticlePodcastPromo.Heading_FieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.ArticlePodcastPromo.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.ArticlePodcastPromo.Body_FieldId)]
        string Body { get; set; }

        [SitecoreField(Constants.ArticlePodcastPromo.BackgroundImage_FieldId)]
        Image BackgroundImage { get; set; }

        [SitecoreField(Constants.ArticlePodcastPromo.BackgroundImageOpacity_FieldId)]
        string BackgroundImageOpacity { get; set; }

        [SitecoreField(Constants.ArticlePodcastPromo.MobileBackgroundImageFieldId)]
        Image MobileBackgroundImage { get; set; }

        [SitecoreField(Constants.ArticlePodcastPromo.PodcastsLabel_FieldId)]
        string PodcastsLabel { get; set; }
        
        [SitecoreChildren]
        IEnumerable<IArticlePodcastLink> PodcastLinks { get; set; }
    }
}