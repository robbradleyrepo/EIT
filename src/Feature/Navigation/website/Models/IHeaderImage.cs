namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeaderImage
    {
        [SitecoreField(Constants.HeaderImage.Image_FieldName)]
        Image Image { get; set; }

        [SitecoreField(Constants.HeaderImage.Title_FieldName)]
        string Title { get; set; }

        [SitecoreField(Constants.HeaderImage.Description_FieldName)]
        string Description { get; set; }

        [SitecoreField(Constants.HeaderImage.Link_FieldName)]
        Link Link { get; set; }
    }
}