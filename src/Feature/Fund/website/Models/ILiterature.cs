namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    public interface ILiterature: IFundGlassBase
    {
        [SitecoreField(Constants.Literature.HeadingFieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.Literature.FundFieldId)]
        IFund Fund { get; set; }

        [SitecoreField(Constants.Literature.CtaFieldId)]
        Link Cta { get; set; }
    }
}