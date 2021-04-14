namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFooterConfiguration
    {
        [SitecoreField(Constants.FooterConfiguration.PageLinks_FieldName)]
        IEnumerable<IPageLink> PageLink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SocialLinks_FieldName)]
        IEnumerable<ISocialIcon> SocialIcons { get; set; }
    }
}