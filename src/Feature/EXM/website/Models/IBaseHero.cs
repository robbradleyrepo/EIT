namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IBaseHero : IExmGlassBase
    {
        [SitecoreField(Constants.BaseHero.Logo_FieldID)]
        Image Logo { get; set; }

        [SitecoreField(Constants.BaseHero.LogoLink_FieldID)]
        Link LogoLink { get; set; }

        [SitecoreField(Constants.BaseHero.Title_FieldID)]
        string Title { get; set; }
    }
}