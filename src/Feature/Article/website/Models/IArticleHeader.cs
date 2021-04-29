namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;

    public interface IArticleHeader: IGlassBase
    {
        [SitecoreField(Constants.ArticleHeader.BackgroundImageFieldId)]
        Image BackgroundImage { get; set; }
    }
}