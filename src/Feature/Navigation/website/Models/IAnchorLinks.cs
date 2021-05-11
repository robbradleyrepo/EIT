namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Navigation.Models;
    using LionTrust.Foundation.ORM.Models;
    using System;
    using System.Collections.Generic;

    public interface IAnchorLinks: IGlassBase
    {
        IEnumerable<IAnchor> Children { get; set; }

        [SitecoreField(Constants.AnchorLinks.HeadingFieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.AnchorLinks.CtaFieldId)]
        Link Cta { get; set; }

        [SitecoreField(Constants.AnchorLinks.CtaGoalFieldId)]
        Guid GoalId { get; set; }
    }
}