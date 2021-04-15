using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data;

namespace LionTrust.Feature.LeadBanner.Models
{
    public class LeadBannerModel
    {   
        public virtual ID Id { get; set; }

        [SitecoreField("LeadBanner_Title")]
        public virtual string Title { get; set; }

        [SitecoreField("LeadBanner_Body")]
        public virtual string Body { get; set; }

        [SitecoreField("LeadBanner_Icon")]
        public virtual Image Icon { get; set; }

        [SitecoreField("LeadBanner_BackgroundImage")]
        public virtual Image BackgroundImage { get; set; }

        [SitecoreField("LeadBanner_TextAlign")]
        public virtual ILookupValue TextAlign { get; set; }

        public string TextAlignValue 
        { 
            get
            { 
                if (TextAlign == null)
                {
                    return string.Empty;
                }

                return TextAlign.Value;
            }
        }
    }
}