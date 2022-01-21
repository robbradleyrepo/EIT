namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System.Collections.Generic;

    public interface IWebinarCards : IExmGlassBase
    {
        [SitecoreField(Constants.WebinarCards.Title_FieldID)]
        string Title { get; set; }

        [SitecoreField(Constants.WebinarCards.Description_FieldID)]
        string Description { get; set; }

        [SitecoreField(Constants.WebinarCards.Webinars_FieldID)]
        IList<IWebinar> Webinars { get; set; }

        [SitecoreField(Constants.WebinarCards.CardCtaText_FieldID)]
        string CardCtaText { get; set; }

        [SitecoreField(Constants.WebinarCards.CtaLink_FieldID)]
        Link CtaLink { get; set; }

        [SitecoreField(Constants.WebinarCards.CtaText_FieldID)]
        string CtaText { get; set; }
    }
}