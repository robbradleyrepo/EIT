namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IPromoBanner : IExmGlassBase
    {
        [SitecoreField(Constants.PromoBanner.Title_FieldID)]
        string Title { get; set; }

        [SitecoreField(Constants.PromoBanner.Description_FieldID)]
        string Description { get; set; }

        [SitecoreField(Constants.PromoBanner.Image_FieldID)]
        Image Image { get; set; }

        [SitecoreField(Constants.PromoBanner.CtaLink_FieldID)]
        Link CtaLink { get; set; }

        [SitecoreField(Constants.PromoBanner.CtaImage_FieldID)]
        Image CtaImage { get; set; }
    }
}