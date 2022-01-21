namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface ICard : IExmGlassBase
    {
        [SitecoreField(Constants.Card.Title_FieldID)]
        string Title { get; set; }

        [SitecoreField(Constants.Card.Description_FieldID)]
        string Description { get; set; }

        [SitecoreField(Constants.Card.Image_FieldID)]
        Image Image { get; set; }

        [SitecoreField(Constants.Card.CtaLink_FieldID)]
        Link CtaLink { get; set; }

        [SitecoreField(Constants.Card.CtaText_FieldID)]
        string CtaText { get; set; }
    }
}