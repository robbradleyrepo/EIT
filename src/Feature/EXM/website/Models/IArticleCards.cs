namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IArticleCards : IExmGlassBase
    {
        [SitecoreField(Constants.ArticleCards.Title_FieldID)]
        string Title { get; set; }

        [SitecoreField(Constants.ArticleCards.NumberListingArticles_FieldID)]
        int NumberListingArticles { get; set; }

        [SitecoreField(Constants.ArticleCards.ArticleCtaImage_FieldID)]
        Image CtaImage { get; set; }
    }
}