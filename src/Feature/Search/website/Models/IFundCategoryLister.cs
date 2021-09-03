namespace LionTrust.Feature.Search.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Search.Models.API.Facets;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.ORM.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IFundCategoryLister : ISearchGlassBase
    {
        [SitecoreField(Constants.FundCategoryLister.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.FundCategoryLister.FundLiterature_FieldId)]
        IGlassBase FundLiterature { get; set; }

        [SitecoreField(Constants.FundCategoryLister.AllDocumentsCtaText_FieldId)]
        string AllDocumentsLabel { get; set; }

        [SitecoreField(Constants.FundCategoryLister.FollowCtaText_FieldId)]
        string FollowLabel { get; set; }

        [SitecoreField(Constants.FundCategoryLister.FactsheetCtaText_FieldId)]
        string FactsheetLabel { get; set; }

        [SitecoreField(Constants.FundCategoryLister.ViewFundCtaText_FieldId)]
        string ViewFundCtaText { get; set; }

        [SitecoreField(Constants.FundCategoryLister.Funds_FieldId)]
        IEnumerable<Guid> Funds { get; set; }
    }
}