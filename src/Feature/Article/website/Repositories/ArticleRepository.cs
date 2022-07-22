namespace LionTrust.Feature.Article.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Foundation.Navigation.Helpers;
    using LionTrust.Foundation.Schema.Models;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;

    public class ArticleRepository
    {
        private readonly IArticleContentSearchService _searchService;
        private readonly IMvcContext _mvcContext;

        public ArticleRepository(IArticleContentSearchService searchService, IMvcContext mvcContext)
        {
            this._searchService = searchService;
            this._mvcContext = mvcContext;
        }

        public IEnumerable<IArticlePromo> GetArticlePromosByTopics(IEnumerable<string> topics, IEnumerable<string> fundManagers = null)
        {
            var request = new ArticleSearchRequest
            {
                Categories = topics,
                Take = int.MaxValue,
                DatabaseName = _mvcContext.SitecoreService.Database.Name,
                FundManagers = fundManagers
            };

            var results = _searchService.GetDatedTaxonomyRelatedArticles(request, result => result.OrderByDescending(hit => hit.Created));
            if (results == null || results.SearchResults == null)
            {
                return null;
            }

            return results.SearchResults
                .Where(sr => sr.Document != null)
                .Select(sr => BuildArticle(sr.Document));
        }

        public IEnumerable<IArticlePromo> Map(IEnumerable<Guid> funds, IEnumerable<Guid> categories, IEnumerable<Guid> fundTeams, IEnumerable<Guid> fundManagers, IEnumerable<Guid> topics, string databaseName)
        {
            var request = new ArticleSearchRequest
            {
                Categories = topics?.Select(t => t.ToString().Replace("-", string.Empty)),
                Funds = funds?.Select(f => f.ToString().Replace("-", string.Empty)),
                ContentTypes = categories?.Select(fc => fc.ToString().Replace("-", string.Empty)),
                FundTeams = fundTeams?.Select(ft => ft.ToString().Replace("-", string.Empty)),
                FundManagers = fundManagers?.Select(fm => fm.ToString().Replace("-", string.Empty)),
                Take = 6,
                DatabaseName = databaseName
            };

            var results = _searchService.GetDatedTaxonomyRelatedArticles(request, result => result.OrderByDescending(hit => hit.ArticleCreatedDate));

            if (results == null || results.SearchResults == null)
            {
                return new IArticlePromo[0];
            }

            return results.SearchResults
                .Where(sr => sr.Document != null)
                .Select(sr => BuildArticle(sr.Document));
        }

        public IEnumerable<IArticlePromo> Map(IArticleFilter filter, string databaseName)
        {
            return Map(filter.Funds?.Select(f => f.Id), filter.ContentTypes?.Select(fc => fc.ArticleType), filter.FundTeams?.Select(ft => ft.Id), filter.FundManagers?.Select(fm => fm.Id), filter.Topics?.Select(t => t.Id), databaseName);
        }

        public ArticleSchema GetArticleSchemaData(IArticle article)
        {
            var articleSchema = new ArticleSchema
            {
                Headline = article.Title,
                Url = article.AbsoluteUrl,
                Description = article.PageShortDescription,
                DatePublished = article.Date.Equals(DateTime.MinValue) ? article.CreatedDate : article.Date,
                DateModified = article.ModifiedDate
            };

            var mediaOption = new MediaUrlOptions()
            {
                AlwaysIncludeServerUrl = true,
                AbsolutePath = true,
                LowercaseUrls = true,
                RequestExtension = ""
            };

            if (article.Image != null)
            {
                var imageItem = _mvcContext.SitecoreService.GetItem<Item>(article.Image.MediaId);
                if (imageItem != null)
                {
                    articleSchema.ImageUrl = MediaManager.GetMediaUrl(imageItem, mediaOption);
                }
            }

            var authorList = new List<string>();
            if (article.Authors != null)
            {
                foreach (var author in article.Authors)
                {
                    authorList.Add(author.FullName);
                }
            }
            articleSchema.Authors = authorList;

            articleSchema.ArticleBody = _searchService.GetArticleContent(article.Id);

            var identityModel = NavigationHelper.GetWebsiteIdentity(_mvcContext, Sitecore.Context.Item);
            if (identityModel == null)
            {
                return articleSchema;
            }

            articleSchema.PublisherName = identityModel.CompanyName;
            if (identityModel.Logo == null)
            {
                return articleSchema;
            }

            var logoItem = _mvcContext.SitecoreService.GetItem<Item>(identityModel.Logo.MediaId);
            if (logoItem != null)
            {
                articleSchema.LogoUrl = MediaManager.GetMediaUrl(logoItem, mediaOption);
            }

            return articleSchema;
        }

        private IArticlePromo BuildArticle(ArticleSearchResultItem hit)
        {
            if (hit == null)
            {
                return null;
            }

            var item = hit.GetItem();
            if (item == null)
            {
                return null;
            }

            return _mvcContext.SitecoreService.GetItem<IArticlePromo>(item.ID.Guid);
        }
    }
}