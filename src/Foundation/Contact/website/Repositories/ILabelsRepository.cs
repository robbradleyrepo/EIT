namespace LionTrust.Foundation.Contact.Repositories
{
    using LionTrust.Foundation.Contact.Models.Labels;

    public interface ILabelsRepository
    {
        FundLabels GetFundLabels();
        GenericLabels GetGenericLabels();
        SharePriceLabels GetSharePriceLabels();
        EditEmailPreferencesLabels GetEmailPreferenceLabels();
        RegisterNonProfUserLabels GetRegisterNonProfUserLabels();
        RegisterProfUserLabels GetRegisterProfUserLabels();
        ListingFilterLabels GetListingFilterLabels();
        SearchResultPageLabels GetSearchResultPageLabels();               
        PersonalizedContentComponentLabels GetPersonalizedContentComponentLabels();
    }
}
