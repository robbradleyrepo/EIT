namespace LionTrust.Feature.EXM.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Navigation.Models;

    public interface IFooter : IExmGlassBase
    {
        [SitecoreField(Constants.Footer.Logo_FieldID)]
        Image Logo { get; set; }

        [SitecoreField(Constants.Footer.LogoLink_FieldID)]
        Link LogoLink { get; set; }

        [SitecoreField(Constants.Footer.Address_FieldID)]
        string Address { get; set; }

        [SitecoreField(Constants.Footer.Location_FieldID)]
        string Location { get; set; }

        [SitecoreField(Constants.Footer.PostalCode_FieldID)]
        string PostalCode { get; set; }

        [SitecoreField(Constants.Footer.PageLinks1_FieldId)]
        IEnumerable<IPageLink> PageLinks1 { get; set; }

        [SitecoreField(Constants.Footer.PageLinks2_FieldId)]
        IEnumerable<IPageLink> PageLinks2 { get; set; }

        [SitecoreField(Constants.Footer.SocialLinks_FieldId)]
        IEnumerable<ISocialIcon> SocialIcons { get; set; }
    }
}