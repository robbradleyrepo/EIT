namespace LionTrust.Foundation.Contact.Models.Types
{
    using Sitecore.Data;
    using Sitecore.Data.Items;

    public class DropLink
    {
        public ID ItemId { get; set; }
        public string ItemName { get; set; }
        public Item SitecoreItem { get; set; }
    }
}
