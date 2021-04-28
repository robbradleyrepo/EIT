namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeaderImage : INavigationGlassBase
    {
        [SitecoreField(Constants.HeaderImage.Image_FieldID, SitecoreFieldType.Image, "Content")]
        Image Image { get; set; }

        [SitecoreField(Constants.HeaderImage.Title_FieldID, SitecoreFieldType.SingleLineText, "Content")]
        string Title { get; set; }

        [SitecoreField(Constants.HeaderImage.Description_FieldID, SitecoreFieldType.SingleLineText, "Content")]
        string Description { get; set; }

        [SitecoreField(Constants.HeaderImage.Link_FieldID, SitecoreFieldType.GeneralLink, "Content")]
        Link Link { get; set; }
    }
}