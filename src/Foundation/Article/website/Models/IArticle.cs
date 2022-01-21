namespace LionTrust.Foundation.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IArticle : Legacy.Models.IArticle
    {
        [SitecoreField(Constants.Article.Image_FieldId)]
        Image Image { get; set; }
    }
}
