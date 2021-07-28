namespace LionTrust.Foundation.Contact.Models.Labels
{
    using LionTrust.Foundation.Contact.Models.Types;

    public class GenericLabels
    {
        public IProperty<string> BackToTop { get; set; }
        public IProperty<string> Email { get; set; }
        public IProperty<string> Mobile { get; set; }
        public IProperty<string> Telephone { get; set; }
        
        public IProperty<string> PreviousLinkText { get; set; }
        public IProperty<string> NextLinkText { get; set; }

        public IProperty<string> ProfilePageLinkText { get; set; }
    }
}
