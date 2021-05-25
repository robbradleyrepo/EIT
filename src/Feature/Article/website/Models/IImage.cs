namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IImage: IArticleGlassBase
    {
        [SitecoreField(Constants.Image.ImageFieldId)]
        Image Image { get; set; }
        [SitecoreField(Constants.Image.OpacityFieldId)]
        string Opacity { get; set; }
    }
}
