namespace LionTrust.Foundation.Contact.Models.Labels
{
    using LionTrust.Foundation.Contact.Models.Types;

    /// <summary>
    /// Labels for personalized component
    /// </summary>
    public class PersonalizedContentComponentLabels
    {
        public IProperty<string> EditEmailPreferenceButtonLabel { get; set; }        
        public IProperty<string> FundsFilterHeading { get; set; }        
        public IProperty<string> FundsFilterDefaultOptionText { get; set; }         
        public IProperty<string> BlogArticleSectionTitle { get; set; }
        public IProperty<string> BlogArticleTitleTableHeading { get; set; }
        public IProperty<string> BlogArticleSubtitleTableHeading { get; set; }
        public IProperty<string> BlogArticleCreatedDateTableHeading { get; set; }
        public IProperty<string> BlogArticleViewTableHeading { get; set; }
        public IProperty<string> BlogArticleViewLabel { get; set; }
        public IProperty<string> FundDocumentSectionTitle { get; set; }
        public IProperty<string> DocumentNameTableHeading { get; set; }
        public IProperty<string> FundNameTableHeading { get; set; }
        public IProperty<string> DownloadTableHeading { get; set; }
        public IProperty<string> DownloadAllLabel { get; set; }        
        public IProperty<string> SalesforceFundPreferencesNotFoundErrorMessage { get; set; }
        public IProperty<string> ComponentDataLoadingErrorMessage { get; set; }
    }
}
