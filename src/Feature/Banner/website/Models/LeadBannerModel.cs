namespace LionTrust.Feature.Banner.Models
{
    using Foundation.ORM.Extensions;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using Sitecore.Data;

    public class LeadBannerModel
    {   
        public virtual ID Id { get; set; }

        [SitecoreField("LeadBanner_Title")]
        public virtual string Title { get; set; }

        [SitecoreField("LeadBanner_Body")]
        public virtual string Body { get; set; }

        [SitecoreField("LeadBanner_BackgroundImage")]
        public virtual Image BackgroundImage { get; set; }

        public string BackgroundImageStyle
        {
            get
            {
                return BackgroundImage?.GetSafeBackgroundImageStyle();
            }
        }
    }
}