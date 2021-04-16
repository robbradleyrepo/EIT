using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using LionTrust.Foundation.ORM.Models;
using Sitecore.Data;
using System;

namespace LionTrust.Feature.Newsletter.Promo
{
    public interface INewsletterPromoModel: IGlassBase
    {
        [SitecoreField("NewsletterPromo_Heading")]
        string Heading { get; set; }

        [SitecoreField("NewsletterPromo_Body")]
        string Body { get; set; }

        [SitecoreField("NewsletterPromo_CTA")]
        Link Cta { get; set; }

        [SitecoreField("NewsletterPromo_CTAGoal")]
        string Goal { get; set; }
      

        [SitecoreField("NewsletterPromo_BackgroundImage")]
        Image BackgroundImage { get; set; }
    }
}