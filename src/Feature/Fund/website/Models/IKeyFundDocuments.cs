namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IKeyFundDocuments
    {
        [SitecoreField(Constants.KeyFundDocuments.Title_FieldID, SitecoreFieldType.SingleLineText, "Key Fund Documents")]
        string Title { get; set; }

        [SitecoreField(Constants.KeyFundDocuments.Description_FieldID, SitecoreFieldType.MultiLineText, "Key Fund Documents")]
        string Description { get; set; }

        [SitecoreField(Constants.KeyFundDocuments.KeyDocuments_FieldID, SitecoreFieldType.Treelist, "Key Fund Documents")]
        string KeyDocuments { get; set; }
    }
}