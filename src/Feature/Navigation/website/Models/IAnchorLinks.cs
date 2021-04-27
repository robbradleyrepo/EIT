namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    public interface IAnchorLinks: IGlassBase
    {
        IEnumerable<ILinkWithGoal> Children { get; set; }

        [SitecoreField(Constants.AnchorLinks.HeadingFieldId)]
        string Heading { get; set; }
    }
}