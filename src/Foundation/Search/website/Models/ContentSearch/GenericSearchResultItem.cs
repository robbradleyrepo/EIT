namespace LionTrust.Foundation.Search.Models.ContentSearch
{
    using System;

    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;
    
    public class GenericSearchResultItem : BaseSearchResultItem
    {
        [IndexField("genericlistingmoduleitem_image")]
        public string GenericListingImage { get; set; }

        [IndexField("genericlistingmoduleitem_imagewidth")]
        public string GenericListingImageWidth { get; set; }

        [IndexField("genericlistingmoduleitem_richtext")]
        public string GenericListingText { get; set; }

        [IndexField("genericlistingmoduleitem_title")]
        public string GenericListingTitle { get; set; }

        [IndexField("genericlistingmoduleitem_subtitle")]
        public string GenericListingSubtitle { get; set; }

        [IndexField("genericlistingmoduleitem_listingtype")]
        public string GenericListingType { get; set; }

        [IndexField("genericlistingmoduleitem_year")]
        public int Year { get; set; }

        [IndexField("genericlistingmoduleitem_month")]
        public int Month { get; set; }

        [IndexField("_parent")]
        public Guid GenericListingParent { get; set; }

        [IndexField("genericlistingmoduleitem_date")]
        public DateTime Date { get; set; }
    }
}