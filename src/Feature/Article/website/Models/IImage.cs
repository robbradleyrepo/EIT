namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IImage: IArticleGlassBase
    {
        [SitecoreField(Constants.Image.ImageFieldId)]
        Image Image { get; set; }
    }
}
