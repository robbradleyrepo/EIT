namespace LionTrust.Foundation.Contact.Models.Labels
{
    using LionTrust.Foundation.Contact.Models.Types;

    public class RegisterProfUserLabels
    {
        public IProperty<string> Title { get; set; }
        public IProperty<string> Subtitle { get; set; }
        public IProperty<string> Description { get; set; }
        public IProperty<string> DefaultSFOrganisationId { get; set; }
        public IProperty<string> GenericErrorLabel { get; set; }
        public IProperty<string> UserExistsErrorLabel { get; set; }
    }
}
