namespace LionTrust.Feature.Listings.Models
{
    using System;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    
    [SitecoreType(TemplateId = Constants.GenericListingModuleItem.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface IGenericListingModuleItem : IListingsGlassBase
    {
        [SitecoreField(Constants.GenericListingModuleItem.Title_FieldID)]
        string Title { get; set; }

        [SitecoreField(Constants.GenericListingModuleItem.Subtitle_FieldID)]
        string Subtitle { get; set; }

        [SitecoreField(Constants.GenericListingModuleItem.Image_FieldID)]
        string Image { get; set; }

        [SitecoreField(Constants.GenericListingModuleItem.Text_FieldID)]
        string Text { get; set; }

        [SitecoreField(Constants.GenericListingModuleItem.Date_FieldID)]
        DateTime Date { get; set; }
    }
}