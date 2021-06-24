namespace LionTrust.Feature.Search.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IPagination : ISearchGlassBase
    {
        [SitecoreField(Constants.Pagination.NextLabel_FieldId)]
        string NextLabel { get; set; }

        [SitecoreField(Constants.Pagination.PreviousLabel_FieldId)]
        string PreviousLabel { get; set; }
    }
}