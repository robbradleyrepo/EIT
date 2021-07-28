using LionTrust.Foundation.Contact.Models.Labels;

namespace LionTrust.Foundation.Contact.Repositories
{
    public class LabelsRepository : BaseRepository, ILabelsRepository
    {
        public LabelsRepository(IEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public FundLabels GetFundLabels()
        {
            var fundLabelsItem = GetItem(Constants.ItemIds.Content.Labels.FundLabels);

            return EntityFactory.Build<FundLabels>(fundLabelsItem);
        }

        public GenericLabels GetGenericLabels()
        {
            var genericLabelsItem = GetItem(Constants.ItemIds.Content.Labels.GenericLabels);

            return EntityFactory.Build<GenericLabels>(genericLabelsItem);
        }

        public SharePriceLabels GetSharePriceLabels()
        {
            var sharePriceLabelsItem = GetItem(Constants.ItemIds.Content.Labels.SharePriceLabels);

            return EntityFactory.Build<SharePriceLabels>(sharePriceLabelsItem);
        }

        public EditEmailPreferencesLabels GetEmailPreferenceLabels()
        {
            var editEmailPreferenceLabelItem = GetItem(Constants.ItemIds.Content.Labels.EditEmailPreferenceLabels);
            return EntityFactory.Build<EditEmailPreferencesLabels>(editEmailPreferenceLabelItem);
        }

        public RegisterNonProfUserLabels GetRegisterNonProfUserLabels()
        {
            var registerNonProfUserLabelItem = GetItem(Constants.ItemIds.Content.Labels.RegisterNonprofUserLabels);
            return EntityFactory.Build<RegisterNonProfUserLabels>(registerNonProfUserLabelItem);
        }

        public RegisterProfUserLabels GetRegisterProfUserLabels()
        {
            var registerProfUserLabelItem = GetItem(Constants.ItemIds.Content.Labels.RegisterProfUserLabels);
            return EntityFactory.Build<RegisterProfUserLabels>(registerProfUserLabelItem);
        }

        public ListingFilterLabels GetListingFilterLabels()
        {
            var listingFilterLabelItem = GetItem(Constants.ItemIds.Content.Labels.ListingFilterLabels);
            return EntityFactory.Build<ListingFilterLabels>(listingFilterLabelItem);
        }

        public SearchResultPageLabels GetSearchResultPageLabels()
        {
            var serachresultPageLabelItem = GetItem(Constants.ItemIds.Content.Labels.SearchResultPageLabels);
            return EntityFactory.Build<SearchResultPageLabels>(serachresultPageLabelItem);
        }
        
        public PersonalizedContentComponentLabels GetPersonalizedContentComponentLabels()
        {
            var labelItem = GetItem(Constants.ItemIds.Content.Labels.PersonalizedContentComponentLabels);
            return EntityFactory.Build<PersonalizedContentComponentLabels>(labelItem);
        }
    }
}
