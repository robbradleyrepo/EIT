namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using System.Linq;

    public class ArticlePodcast : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            var itemChildrens = item.GetChildren();
            if (itemChildrens == null || !itemChildrens.Any())
            {
                return string.Empty;
            }

            var articlePodcast = itemChildrens.Where(x => x != null && x.TemplateID.Equals(Constants.PodcastTemplateId))?.FirstOrDefault();
            if (articlePodcast == null)
            {
                return string.Empty;
            }

            return articlePodcast.ID.ToString();
        }
    }
}