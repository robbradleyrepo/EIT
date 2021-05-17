namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IArticleRichText: IArticleGlassBase
    {
        [SitecoreField(Constants.ArticleRichText.CopyFieldId)]
        string Copy { get; set; }
    }
}
