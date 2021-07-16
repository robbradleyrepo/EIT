namespace LionTrust.Foundation.Indexing.ComputedFields.Fund
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Fields;

    public class FundTeamName : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            var fundTeam = (LookupField)item.Fields[Legacy.Constants.Fund.FundTeamFieldId];
            var fundTeamName = ComputedValueHelper.GetDropLinkFieldValue(fundTeam, Legacy.Constants.FundTeam.NameFieldId);

            return fundTeamName;
        }
    }
}

