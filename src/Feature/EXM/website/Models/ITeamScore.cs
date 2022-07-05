namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface ITeamScore : IExmGlassBase
    {
        [SitecoreField(Constants.TeamScore.TeamOrganizationId_FieldID)]
        string TeamOrganizationId { get; set; }

        [SitecoreField(Constants.TeamScore.EmailOpenedPoints_FieldID)]
        int EmailOpenedPoints { get; set; }

        [SitecoreField(Constants.TeamScore.ClickedThroughPoints_FieldID)]
        int ClickedThroughPoints { get; set; }
    }
}