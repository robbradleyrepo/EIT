using System;
using System.Collections.Generic;

namespace LionTrust.Foundation.Schema.Models
{
    public class ArticleSchema
    {
        public string Headline { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime DateModified { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public List<string> Authors { get; set; }
        public string ArticleBody { get; set; }
        public string PublisherName { get; set; }
        public string LogoUrl { get; set; }     
    }
}