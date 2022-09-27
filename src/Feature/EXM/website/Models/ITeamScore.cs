namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface ITeamScore : IExmGlassBase
    {
        [SitecoreField(Constants.TeamScore.SalesforceFieldId_FieldID)]
        string SalesforceFieldId { get; set; }

        [SitecoreField(Constants.TeamScore.EmailOpenedPoints_FieldID)]
        int EmailOpenedPoints { get; set; }

        [SitecoreField(Constants.TeamScore.ClickedThroughPoints_FieldID)]
        int ClickedThroughPoints { get; set; }
    }
}