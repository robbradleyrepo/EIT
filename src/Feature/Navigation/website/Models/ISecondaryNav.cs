namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    public interface ISecondaryNav: IGlassBase
    {
        IEnumerable<ILinkWithGoal> Children { get; set; }

        [SitecoreField(Constants.SecondaryNavigation.HeadingFieldId)]
        string Heading { get; set; }
    }
}