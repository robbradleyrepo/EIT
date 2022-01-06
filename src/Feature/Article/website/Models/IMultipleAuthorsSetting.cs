namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IMultipleAuthorsSetting : IArticleGlassBase
    {
        [SitecoreField(Constants.MultipleAuthorsSetting.Label_FieldId)]
        string Label { get; set; }

        [SitecoreField(Constants.MultipleAuthorsSetting.Icon_FieldId)]
        Image Icon { get; set; }    
    }
}