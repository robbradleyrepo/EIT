namespace LionTrust.Feature.Fund.CreditRating
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Fund.Models;

    public interface ICreditRatingRow: IFundGlassBase
    {
        [SitecoreField(Constants.CreditRatingRow.NameFieldId)]
        string RowName { get; set; }

        [SitecoreField(Constants.CreditRatingRow.ValueFieldId)]
        string Value { get; set; }
    }
}