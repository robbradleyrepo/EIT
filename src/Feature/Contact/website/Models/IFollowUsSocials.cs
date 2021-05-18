namespace LionTrust.Feature.Contact.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IFollowUsSocials : IContactGlassBase
    {
        [SitecoreField(Constants.FollowUsSocial.Heading_FieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.FollowUsSocial.Description_FieldId)]
        string Description { get; set; }

        [SitecoreField(Constants.FollowUsSocial.FirstSocialIcon_FieldId)]
        Image FirstSocialIcon { get; set; }

        [SitecoreField(Constants.FollowUsSocial.FirstSocialLink_FieldId)]
        Link FirstSocialLink { get; set; }

        [SitecoreField(Constants.FollowUsSocial.FirstTwitterTags_FieldId)]
        string FirstTwitterTags { get; set; }

        [SitecoreField(Constants.FollowUsSocial.SecondSocialIcon_FieldId)]
        Image SecondSocialIcon { get; set; }

        [SitecoreField(Constants.FollowUsSocial.SecondSocialLink_FieldId)]
        Link SecondSocialLink { get; set; }

        [SitecoreField(Constants.FollowUsSocial.SecondTwitterTags_FieldId)]
        string SecondTwitterTags { get; set; }

        [SitecoreField(Constants.FollowUsSocial.ThirdSocialIcon_FieldId)]
        Image ThirdSocialIcon { get; set; }

        [SitecoreField(Constants.FollowUsSocial.ThirdSocialLink_FieldId)]
        Link ThirdSocialLink { get; set; }

        [SitecoreField(Constants.FollowUsSocial.ThirdTwitterTags_FieldId)]
        string ThirdTwitterTags { get; set; }
    }
}