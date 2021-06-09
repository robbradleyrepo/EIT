namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;

    public interface IFeaturedArticleHero : IGlassBase    
    {
        [SitecoreField(Constants.FeaturedArticleHero.Article_FieldId)]
        IArticle Article { get; set; }

        [SitecoreField(Constants.FeaturedArticleHero.BackgroundImage_FieldId)]
        Image BackgroundImage { get; set; }

        [SitecoreField(Constants.FeaturedArticleHero.ArticleLinkText_FieldId)]
        string LinkText { get; set; }
    }
}
