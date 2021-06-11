namespace LionTrust.Feature.Fund.CreditRating
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Fund.Models;
    using System.Collections.Generic;

    public interface ICreditRating: IFundGlassBase
    {
        [SitecoreField(Constants.CreditRating.HeadingFieldId)]
        string Heading { get; set; }

        IEnumerable<ICreditRatingRow> Children { get; set; }

        [SitecoreField(Constants.CreditRating.MaxValueFieldId)]
        string MaxValue { get;set; }

        string JsonDataObject { get; set; }
    }
}
