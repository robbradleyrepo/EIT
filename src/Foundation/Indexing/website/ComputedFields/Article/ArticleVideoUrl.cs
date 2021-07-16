namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Fields;
    using Sitecore.Sites;

    public class ArticleVideoUrl : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            var videoUrl = string.Empty;
            if (item != null)
            {                
                var videoUrlField = (LinkField)item.Fields[Constants.ArticleVideoUrl];

                if (videoUrlField != null)
                {
                    using (new SiteContextSwitcher(Factory.GetSite(Constants.SiteName)))
                    {
                        videoUrl = videoUrlField.Url;                        
                    }
                }
            }

            return videoUrl;
        }
    }
}