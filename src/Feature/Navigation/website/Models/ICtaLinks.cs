namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Navigation.Models;
    using LionTrust.Foundation.ORM.Models;
    using System;
    using System.Collections.Generic;

    public interface ICtaLinks: IGlassBase
    {
        IEnumerable<ICtaLink> Children { get; set; }

        [SitecoreField(Constants.CtaLinks.HeadingFieldId)]
        string Heading { get; set; }
    }
}