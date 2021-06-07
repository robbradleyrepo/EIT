namespace LionTrust.Feature.Contact.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface ILocationAndMap : IContactGlassBase
    {
        [SitecoreField(Constants.LocationAndMap.Heading_FieldId)]
        string Heading { get; set; }        

        [SitecoreField(Constants.LocationAndMap.SelectLabel_FieldId)]
        string SelectLabel { get; set; }

        [SitecoreChildren]
        IEnumerable<IMapLink> MapLinks { get; set; }
    }
}