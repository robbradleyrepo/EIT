namespace LionTrust.Foundation.Contact.Models.Labels
{
    using LionTrust.Foundation.Contact.Models.Types;

    public class SharePriceLabels
    {
        public IProperty<string> TitleText { get; set; }
        public IProperty<string> SubTitleText { get; set; }       
        public IProperty<string> NoDataExceptionText { get; set; } 
    }
}
