namespace LionTrust.Feature.Article.Fields
{
    using LionTrust.Foundation.Indexing.SiteSearch;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Sitecore.Data.Items;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using LionTrust.Foundation.DI;

    [Service(ServiceType = typeof(IPageAuthorField), Lifetime = Lifetime.Singleton)]
    public class ArticleAuthorField : IPageAuthorField
    {
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(Constants.ArticlePage.ArticlePageTemplateId);
        }

        public string GetAuthorName(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var multipleAuthorsSetting = (LookupField)item?.Fields[Foundation.Legacy.Constants.Article.MultipleAuthorsSetting_FieldId];
            var multipleAuthorsSettingItem = multipleAuthorsSetting?.TargetItem;
            var overrideValue = multipleAuthorsSettingItem[Foundation.Legacy.Constants.Article.MultipleAuthorsSettingLabel_FieldId];
            if (!string.IsNullOrEmpty(overrideValue))
            {
                return overrideValue;
            }

            var author = (MultilistField) item.Fields[new ID(Foundation.Legacy.Constants.Article.Authors_FieldId)];
            if (author == null || author.Count == 0)
            {
                return null;
            }

            var authorItem = item.Database.GetItem(author[0]); 
            if (authorItem == null)
            {
                return null;
            }

            return authorItem[Foundation.Legacy.Constants.Author.FullName_FieldId];
        }
    }
}