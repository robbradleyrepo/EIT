namespace LionTrust.Feature.Promo.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IPromoLookup : IPromoGlassBase
    {
        [SitecoreField(Constants.PromoLookup.Value_FieldId, SitecoreFieldType.Checkbox, "Promo Lookup")]
        string Value { get; set; }
    }
}