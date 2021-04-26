namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IArticleType : ILegacyGlassBase
    {
        [SitecoreField(Constants.ArticleType.Value_FieldId, SitecoreFieldType.SingleLineText, "Article Type")]
        string Value { get; set; }
    }
}