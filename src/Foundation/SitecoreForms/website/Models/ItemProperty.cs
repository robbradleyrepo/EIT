namespace LionTrust.Foundation.SitecoreForms.Models
{
    using System;

    public class ItemProperty
    {
        public string Name { get; set; }
        public Guid SitecoreId { get; set; }
        public string FullPath { get; set; }
        public string LongId { get; set; }
        public Guid TemplateId { get; set; }
    }
}
