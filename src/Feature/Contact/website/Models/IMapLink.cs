namespace LionTrust.Feature.Contact.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IMapLink : IContactGlassBase
    {
        [SitecoreField(Constants.MapLink.MaplinkName_FieldId)]
        string MapLinkName { get; set; }

        [SitecoreField(Constants.MapLink.MapLink_FieldId)]
        Link MapLink { get; set; }

        [SitecoreField(Constants.MapLink.OfficeAddress_FieldId)]
        string OfficeAddress { get; set; }

        [SitecoreField(Constants.MapLink.OfficeCity_FieldId)]
        string OfficeCity { get; set; }

        [SitecoreField(Constants.MapLink.OfficePostalCode_FieldId)]
        string OfficePostalCode { get; set; }

        [SitecoreField(Constants.MapLink.DesktopBackgroundImage_FieldId)]
        Image DesktopBackgroundImage { get; set; }

        [SitecoreField(Constants.MapLink.TabletBackgroundImage_FieldId)]
        Image TabletBackgroundImage { get; set; }

        [SitecoreField(Constants.MapLink.MobileBackgroundImage_FieldId)]
        Image MobileBackgroundImage { get; set; }
    }
}