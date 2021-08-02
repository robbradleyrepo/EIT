namespace LionTrust.Feature.Banner.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface IPeopleInfoBanner : IBannerGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IPeopleInfo> PeopleInfos { get; set; }

        [SitecoreField(Constants.PeopleInfoBanner.BackgroundImage_FieldId)]
        Image BackgroundImage { get; set; }

        [SitecoreField(Constants.PeopleInfoBanner.Description_FieldId)]
        string Description { get; set; }

        [SitecoreField(Constants.PeopleInfoBanner.MaleCountIcon_FieldId)]
        Image MaleCountIcon { get; set; }

        [SitecoreField(Constants.PeopleInfoBanner.FemaleCountIcon_FieldId)]
        Image FemaleCountIcon { get; set; }
    }
}