namespace LionTrust.Foundation.Search.Models.ContentSearch
{
    using System;

    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;
    
    public class GenericSearchResultItem : SearchResultItem
    {
        [IndexField("genericlistingmoduleitem_image")]
        public string GenericListingImage { get; set; }

        [IndexField("genericlistingmoduleitem_text")]
        public string GenericListingText { get; set; }

        [IndexField("genericlistingmoduleitem_title")]
        public string GenericListingTitle { get; set; }

        [IndexField("genericlistingmoduleitem_subtitle")]
        public string GenericListingSubtitle { get; set; }

        [IndexField("genericlistingmoduleitem_listingtype")]
        public string GenericListingType { get; set; }

        [IndexField("__Created")]
        public DateTime Created { get; set; }

        [IndexField("_parent")]
        public Guid GenericListingParent { get; set; }
    }
}