namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IFeaturedArticle : IExmGlassBase
    {
        [SitecoreField(Constants.FeaturedArticle.Title_FieldID)]
        string Title { get; set; }

        [SitecoreField(Constants.FeaturedArticle.Description_FieldID)]
        string Description { get; set; }

        [SitecoreField(Constants.FeaturedArticle.Image_FieldID)]
        Image Image { get; set; }

        [SitecoreField(Constants.FeaturedArticle.CtaLink_FieldID)]
        Link CtaLink { get; set; }

        [SitecoreField(Constants.FeaturedArticle.CtaText_FieldID)]
        string CtaText { get; set; }
    }
}