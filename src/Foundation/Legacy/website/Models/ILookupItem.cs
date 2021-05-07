namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface ILookupItem : ILegacyGlassBase
    {
        [SitecoreField(Constants.LookupItem.ItemName_FieldId, SitecoreFieldType.SingleLineText, "Content")]
        string ItemName { get; set; }

        [SitecoreField(Constants.LookupItem.TagName_FieldId, SitecoreFieldType.SingleLineText, "Content")]
        string TagName { get; set; }
    }
}