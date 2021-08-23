using System;
using System.Collections;
using System.Collections.Generic;
using Sitecore.Globalization;

namespace LionTrust.Foundation.Content.Tests.Models
{
    public class TestItem : ITestItem
    {
        public Guid Id { get; set; }
        public Language Language { get; set; }
        public int Version { get; set; }
        public IEnumerable<Guid> BaseTemplateIds { get; set; }
        public string TemplateName { get; set; }
        public Guid TemplateId { get; set; }
        public string Name { get; set; }

        public string Title { get; set; }
        public string Url { get; set; }
        public string AbsoluteUrl { get; set; }
    }
}
