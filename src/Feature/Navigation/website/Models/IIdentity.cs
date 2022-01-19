namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;

    public interface IIdentity : IGlassBase
    {
        [SitecoreField(Constants.Identity.Logo_FieldID, SitecoreFieldType.Image, "Identity")]
        Image Logo { get; set; }

        [SitecoreField(Constants.Identity.CompanyName_FieldID, SitecoreFieldType.SingleLineText, "Identity")]
        string CompanyName { get; set; }

        [SitecoreField(Constants.Identity.ContactType_FieldID, SitecoreFieldType.SingleLineText, "Identity")]
        string ContactType { get; set; }

        [SitecoreField(Constants.Identity.AreaServed_FieldID, SitecoreFieldType.SingleLineText, "Identity")]
        string AreaServed { get; set; }
    }
}