namespace LionTrust.Feature.Hero.Models
{
    using Glass.Mapper.Sc.Fields;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFundManagerHero : IHeroGlassBase
    {
        [SitecoreField(Constants.FundManagerHero.FundManagerFirstNameFieldId)]
        string FundManagerFirstName { get; set; }

        [SitecoreField(Constants.FundManagerHero.FundManagerSecondNameFieldId)]
        string FundManagerSecondName { get; set; }

        [SitecoreField(Constants.FundManagerHero.FundManagerBackgroundImageFieldId)]
        Image FundManagerBackgroundImage { get; set; }
    }
}
