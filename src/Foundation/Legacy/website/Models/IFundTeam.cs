namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IFundTeam: ILegacyGlassBase
    {
        [SitecoreField(Constants.FundTeam.ProfessionalsFieldId)]
        IAuthor Professionals { get; set; }

        [SitecoreField(Constants.FundTeam.DescriptionFieldId)]
        string Description { get; set; }

        [SitecoreField(Constants.FundTeam.TeamImageFieldId)]
        Image TeamImage { get; set; }

        [SitecoreField(Constants.FundTeam.TeamPageFieldId)]
        ILegacyGlassBase Page { get; set; }

        [SitecoreField(Constants.FundTeam.NameFieldId)]
        string TeamName { get; set; }
    }
}
