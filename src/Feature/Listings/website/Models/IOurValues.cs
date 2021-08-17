namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IOurValues : IListingsGlassBase
    {
        [SitecoreField(Constants.OurValues.Heading_FieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.OurValues.HeadingColor_FieldId)]
        Foundation.Design.ILookupValue HeadingColor { get; set; }

        [SitecoreField(Constants.OurValues.FirstTitle_FieldId)]
        string FirstTitle { get; set; }

        [SitecoreField(Constants.OurValues.FirstBodyCopy_FieldId)]
        string FirstBodyCopy { get; set; }

        [SitecoreField(Constants.OurValues.SecondTitle_FieldId)]
        string SecondTitle { get; set; }

        [SitecoreField(Constants.OurValues.SecondBodyCopy_FieldId)]
        string SecondBodyCopy { get; set; }

        [SitecoreField(Constants.OurValues.ThirdTitle_FieldId)]
        string ThirdTitle { get; set; }

        [SitecoreField(Constants.OurValues.ThirdBodyCopy_FieldId)]
        string ThirdBodyCopy { get; set; }
    }
}