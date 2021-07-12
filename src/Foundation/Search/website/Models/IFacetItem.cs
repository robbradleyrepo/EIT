namespace LionTrust.Foundation.Search.Models
{
    using LionTrust.Foundation.ORM.Models;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFacetItem : IGlassBase
    {
        [SitecoreField(Constants.FacetItem.Name_FieldID)]
        string Title { get; set; }

        [SitecoreField(Constants.FacetItem.Value_FieldID)]
        string Value { get; set; }
    }
}