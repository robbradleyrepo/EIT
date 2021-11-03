namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Video;

    public interface IAccordionRowModel: IVideoModel
    {
        [SitecoreField(Listings.Constants.Accordion.HeadingField)]
        string Header { get; set; }

        [SitecoreField(Listings.Constants.Accordion.CopyField)]
        string Copy { get; set; }

        [SitecoreField(Listings.Constants.Accordion.ImageField)]
        Image Image { get; set; }        
    }
}