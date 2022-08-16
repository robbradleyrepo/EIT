namespace LionTrust.Feature.Contact.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Navigation.Models;
    using System.Collections.Generic;

    public interface IFollowUsSocials : IContactGlassBase
    {
        [SitecoreField(Constants.FollowUsSocial.Heading_FieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.FollowUsSocial.Description_FieldId)]
        string Description { get; set; }

        [SitecoreChildren]
        IEnumerable<ISocialIcon> SocialIcons { get; set; }      
    }
}