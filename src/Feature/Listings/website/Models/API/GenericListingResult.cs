namespace LionTrust.Feature.Listings.Models
{
    using System;

    using LionTrust.Foundation.Search.Models;
    
    public class GenericListingResult : IGenericContentResult
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public string ListingType { get; set; }

        public string Date { get; set; }
    }
}