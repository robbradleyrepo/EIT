namespace LionTrust.Feature.Banner.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IPeopleInfo : IBannerGlassBase
    {
        [SitecoreField(Constants.PeopleInfo.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.PeopleInfo.MaleCount_FieldId)]
        int MaleCount { get; set; }

        [SitecoreField(Constants.PeopleInfo.FemaleCount_FieldId)]
        int FemaleCount { get; set; }
    }
}