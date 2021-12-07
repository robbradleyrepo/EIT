namespace LionTrust.Feature.Article.Fields
{
    using LionTrust.Foundation.Indexing.SiteSearch;
    using Sitecore.Data.Items;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Sitecore.Data.Fields;
    using Sitecore.Data;
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.Services;

    [Service(ServiceType = typeof(IPageAuthorImageField), Lifetime = Lifetime.Singleton)]
    public class ArticleAuthorImageField : IPageAuthorImageField
    {
        private readonly IndexableLinkGenerator _indexableLinkGenerator;

        public ArticleAuthorImageField(IndexableLinkGenerator indexableLinkGenerator)
        {
            this._indexableLinkGenerator = indexableLinkGenerator;
        }

        public bool CanHandle(IEnumerable<Guid> baseTemplateIds)
        {
            return baseTemplateIds.Contains(Constants.ArticlePage.ArticlePageTemplateId);
        }

        public string GetAuthorImageUrl(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var multipleAuthorsSetting = (LookupField)item?.Fields[Foundation.Legacy.Constants.Article.MultipleAuthorsSetting_FieldId];
            var multipleAuthorsSettingItem = multipleAuthorsSetting?.TargetItem;
            var overrideValue = string.Empty; 
            if (multipleAuthorsSettingItem != null)
            {
                overrideValue = multipleAuthorsSettingItem[Foundation.Legacy.Constants.Article.MultipleAuthorsSettingIcon_FieldId];
            }
            
            if (!string.IsNullOrEmpty(overrideValue))
            {
                return overrideValue;
            }

            var author = (MultilistField)item.Fields[new ID(Foundation.Legacy.Constants.Article.Authors_FieldId)];
            if (author == null || author.Count == 0)
            {
                return null;
            }

            var authorItem = item.Database.GetItem(author[0]);
            if (authorItem == null)
            {
                return null;
            }

            var mediaItem = (ImageField) authorItem.Fields[Foundation.Legacy.Constants.Author.Image_FieldId];
            if (mediaItem == null)
            {
                return null;
            }

            return _indexableLinkGenerator.GenerateLink(mediaItem.MediaItem);
        }
    }
}