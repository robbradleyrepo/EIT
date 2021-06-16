namespace LionTrust.Feature.Onboarding.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IProfileCard : IGlassBase
    {
        [SitecoreField(Constants.Analytics.ProfileCardValue_FieldId)]
        string ProfileCardValue { get; set; }
    }
}