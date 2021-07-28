namespace LionTrust.Foundation.Contact.Models.Labels
{
    using LionTrust.Foundation.Contact.Models.Types;

    public class ListingFilterLabels
    {
        public IProperty<string> YearFilterHeading { get; set; }
        public IProperty<string> MonthFilterHeading { get; set; }
        public IProperty<string> ListingTypFilterHeading { get; set; }
        public IProperty<string> YearFilterDefaultOptionText { get; set; }
        public IProperty<string> MonthFilterDefaultOptionText { get; set; }
        public IProperty<string> ListingTypeFilterDefaultOptionText { get; set; }
        public int YearRange { get; set; }
        public IProperty<string> ClearFilterButtonText { get; set; }
    }
}
