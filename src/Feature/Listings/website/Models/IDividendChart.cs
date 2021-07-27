namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IDividendChart : IListingsGlassBase
    {
        [SitecoreField(Listings.Constants.DividendChart.Title_FieldId)]
        public string Title { get; set; }

        [SitecoreField(Listings.Constants.DividendChart.TableContent_FieldId)]
        public string TableContent { get; set; }
    }
}