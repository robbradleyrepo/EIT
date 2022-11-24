namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IQuote: IArticleText
    {
        [SitecoreField(Constants.TextBase.TextColorFieldId)]
        Foundation.Design.ILookupValue TextColor { get; set; }
    }
}
