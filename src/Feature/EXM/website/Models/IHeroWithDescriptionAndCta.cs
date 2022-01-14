namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IHeroWithDescriptionAndCta : IHeroWithCta
    {
        [SitecoreField(Constants.HeroWithDescriptionAndCTA.SubTitle_FieldID)]
        string SubTitle { get; set; }

        [SitecoreField(Constants.HeroWithDescriptionAndCTA.Description_FieldID)]
        string Description { get; set; }
    }
}