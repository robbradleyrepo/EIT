﻿namespace LionTrust.Feature.Search.Models.API
{
    using System;
    using System.Collections.Generic;

    using LionTrust.Foundation.Search.Models;

    public class ArticleResult : ITaxonomyContentResult
    { 
        public string Url { get; set; }

        public IEnumerable<string> Authors { get; set; }

        public string AuthorImageUrl { get; set; }

        public string Category { get; set; }

        public string Content { get; set; }

        public string Date { get; set; }

        public string Fund { get; set; }

        public string ImageUrl { get; set; }

        public string ImageOpacity { get; set; }

        public IEnumerable<string> Team { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }
        
        public IEnumerable<string> Topics { get; set; }

        public string VideoUrl { get; set; }

        public PodcastModel Podcast { get; set; }

        public IEnumerable<string> FundIds { get; set; }
    }
}