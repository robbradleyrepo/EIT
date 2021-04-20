namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;

    public interface IAccordionRowModel: IGlassBase
    {
        [SitecoreField(Constants.Accordion.HeadingField)]
        string Header { get; set; }

        [SitecoreField(Constants.Accordion.CopyField)]
        string Copy { get; set; }

        [SitecoreField(Constants.Accordion.ImageField)]
        Image Image { get; set; }
    }
}