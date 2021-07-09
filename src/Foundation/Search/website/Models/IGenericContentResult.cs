namespace LionTrust.Foundation.Search.Models
{
    using LionTrust.Foundation.Search.JsonExtension;
    using Newtonsoft.Json;
    using System;

    public interface IGenericContentResult
    {
        string Title { get; set; }

        string Subtitle { get; set; }
        
        string Text { get; set; }

        string ImageUrl { get; set; }

        string ListingType { get; set; }

        string Date { get; set; }
    }
}