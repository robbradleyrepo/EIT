namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IArticleText: IArticleGlassBase
    {
        [SitecoreField(Constants.ArticleText.CopyFieldId)]
        string Copy { get; set; }   
    }
}
