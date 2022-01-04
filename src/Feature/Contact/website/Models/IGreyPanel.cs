namespace LionTrust.Feature.Contact.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IGreyPanel : IContactGlassBase
    {
        [SitecoreField(Constants.GreyPanel.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.GreyPanel.Description_FieldId)]
        string Description { get; set; }
    }
}