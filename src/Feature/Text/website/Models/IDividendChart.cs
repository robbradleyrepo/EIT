namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IDividendChart : ITextGlassBase
    {
        [SitecoreField(Constants.DividendChart.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.DividendChart.TableContent_FieldId)]
        string TableContent { get; set; }
    }
}