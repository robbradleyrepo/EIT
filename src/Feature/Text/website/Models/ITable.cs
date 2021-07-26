namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface ITable : ITextGlassBase
    {
        [SitecoreField(Constants.Table.Text_FieldId)]
        string Text { get; set; }

        [SitecoreField(Constants.Table.Value_FieldId)]
        string Value { get; set; }
    }
}