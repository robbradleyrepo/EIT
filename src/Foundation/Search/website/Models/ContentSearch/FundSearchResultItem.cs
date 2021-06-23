namespace LionTrust.Foundation.Search.Models.ContentSearch
{
    using System.Collections.Generic;

    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;

    public class FundSearchResultItem : SearchResultItem
    {       
        [IndexField("LegacyFund_FundManagers")]
        public IEnumerable<string> FundManagers { get; set; }

        [IndexField("LegacyFund_FundTeam")]
        public IEnumerable<string> FundTeam { get; set; }

        [IndexField("FundFacets_FundRange")]
        public IEnumerable<string> FundRange { get; set; }

        [IndexField("FundFacets_FundRegion")]
        public IEnumerable<string> FundRegion { get; set; }

        [IndexField("FundPage_PageUrl")]
        public string FundPageUrl { get; set; }

        [IndexField("FundPage_PageLinkText")]
        public string FundPageLinkText { get; set; }

        [IndexField("FundCard_Heading")]
        public string FundCardHeading { get; set; }

        [IndexField("FundCard_Description")]
        public string FundCardDescription { get; set; }

        [IndexField("Fund_Listing_Image_Protected")]
        public string FundCardImage { get; set; }

        [IndexField("LegacyFund_FundName")]
        public string FundName { get; set; }

        [IndexField("LegacyFund_FundDescription")]
        public string FundDescription { get; set; }

        [IndexField("Fund_FactSheet_Protected")]
        public string FundFactSheet { get; set; }



    }
}