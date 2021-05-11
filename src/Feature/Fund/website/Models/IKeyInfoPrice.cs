namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    using System;

    public interface IKeyInfoPrice : IFundGlassBase, IFund
    {
        [SitecoreField(Constants.KeyInfoPrice.ClassLaunchDate_FieldId)]
        DateTime ClassLaunchDate { get; set; }

        [SitecoreField(Constants.KeyInfoPrice.Comparator_FieldId)]
        string Comparator { get; set; }

        [SitecoreField(Constants.KeyInfoPrice.Duration_FieldId)]
        string Duration { get; set; }

        [SitecoreField(Constants.KeyInfoPrice.OfferPrice_FieldId)]
        string OfferPrice { get; set; }

        [SitecoreField(Constants.KeyInfoPrice.SinglePrice_FieldId)]
        string SinglePrice { get; set; }

        [SitecoreField(Constants.KeyInfoPrice.PriceDate_FieldId)]
        DateTime PriceDate { get; set; }
    }
}