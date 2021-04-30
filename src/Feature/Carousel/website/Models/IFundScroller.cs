namespace LionTrust.Feature.Carousel.Models
{
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Design;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public interface IFundScroller: ICarouselGlassBase
    {
        string Heading { get; set; }

        string Description { get; set; }

        string Subtitle { get; set; }

        IEnumerable<ICarouselGlassBase> Funds { get; set; }

        Link PrimaryCta { get; set; }

        Link SecondaryCta { get; set; }

        Guid PrimaryCtaGoalId { get; set; }

        Guid SecondaryCtaGoalId { get; set; }
    }
}