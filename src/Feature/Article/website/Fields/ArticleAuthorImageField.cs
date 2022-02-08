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

            MultilistField author = item.Fields[new ID(Foundation.Legacy.Constants.Article.Authors_FieldId)];
            if (author == null || author.Count == 0)
            {
                return null;
            }

            if (author.Count > 1)
            {
                LookupField multipleAuthorsSetting = item.Fields[Foundation.Legacy.Constants.Article.MultipleAuthorsSetting_FieldId];
                var multipleAuthorsSettingItem = multipleAuthorsSetting?.TargetItem;
                if (multipleAuthorsSettingItem == null)
                {
                    return null;
                }

                ImageField overrideMediaItem = multipleAuthorsSettingItem.Fields[Foundation.Legacy.Constants.Article.MultipleAuthorsSettingIcon_FieldId];
                if (overrideMediaItem == null)
                {
                    return null;
                }

                return _indexableLinkGenerator.GenerateLink(overrideMediaItem.MediaItem);
            }

            var authorItem = item.Database.GetItem(author[0]);
            if (authorItem == null)
            {
                return null;
            }

            ImageField mediaItem = authorItem.Fields[Foundation.Legacy.Constants.Author.Image_FieldId];
            if (mediaItem == null)
            {
                return null;
            }

            return _indexableLinkGenerator.GenerateLink(mediaItem.MediaItem);
        }
    }
}