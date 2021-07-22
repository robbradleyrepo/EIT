namespace LionTrust.Feature.Search.DataManagers.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Glass.Mapper.Sc;
    using LionTrust.Feature.Search.DataManagers.Interfaces;
    using LionTrust.Feature.Search.Models.API;
    using LionTrust.Feature.Search.Models.API.Facets;
    using LionTrust.Feature.Search.Models.API.Response;
    using LionTrust.Foundation.Article.Models;
    using LionTrust.Foundation.Content.Repositories;
    using LionTrust.Foundation.Search.Models;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Models.Response;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.Globalization;

    public class ArticleSearchDataManager : IArticleSearchDataManager
    {
        private readonly IArticleContentSearchService _articleContentSearchService;
        private readonly IContentRepository _contentRepository;

        public ArticleSearchDataManager(IArticleContentSearchService articleContentSearchService, IContentRepository contentRepository)
        {
            _articleContentSearchService = articleContentSearchService;
            _contentRepository = contentRepository;
        }

        private string GetArticleDate(DateTime indexedDate)
        {
            var label = string.Empty;
            if (indexedDate.Date == DateTime.Today)
            {
                label = Translate.Text("Today");
            }
            else if (DateTime.Today - indexedDate.Date == TimeSpan.FromDays(1))
            {
                label = Translate.Text("Yesterday");
            }

            if (string.IsNullOrEmpty(label))
            {
                label = indexedDate.ToString("D", Sitecore.Context.Culture);
            }

            return label;
        }

        private IEnumerable<ITaxonomyContentResult> MapArticleResultHits(IEnumerable<SearchHit<ArticleSearchResultItem>> hits)
        {
            var result = new List<ITaxonomyContentResult>();
            foreach (var hit in hits)
            {
                var articleResult = new ArticleResult
                {
                    Url = hit.Document.ArticleUrl,
                    Authors = hit.Document.ArticleAuthorNames?.Split('|'),
                    Category = hit.Document.ArticleContentTypeName,
                    Date = this.GetArticleDate(!hit.Document.ArticleDate.Equals(DateTime.MinValue) ? hit.Document.ArticleDate : hit.Document.Created),
                    Fund = hit.Document.ArticleFundName,
                    ImageUrl = hit.Document.ArticleListingImage,
                    ImageOpacity = string.IsNullOrWhiteSpace(hit.Document.ArticleListingImageOpacity) || hit.Document.ArticleListingImageOpacity == "" ? "1" : hit.Document.ArticleListingImageOpacity,
                    Subtitle = hit.Document.ArticleSubtitle,
                    Team = hit.Document.ArticleTeam,
                    Title = hit.Document.ArticleTitle,
                    Topics = hit.Document.TopicNames?.Split('|'),
                    AuthorImageUrl = hit.Document.ArticleAuthorImage,
                    VideoUrl = hit.Document.ArticleVideoUrl,
                    FundId = hit.Document.ArticleFund,
                    Content = hit.Document.ArticleContent
                };

                if (!string.IsNullOrEmpty(hit.Document.ArticlePodcast))
                {
                    try
                    {
                        var podcastId = new Guid(hit.Document.ArticlePodcast);
                        var podcast = _contentRepository.GetItem<IArticlePodcastPromo>(new GetItemByIdOptions(podcastId));
                        if (podcast != null)
                        {
                            articleResult.Podcast = new PodcastModel()
                            {
                                Heading = podcast.Heading,
                                Title = podcast.Title,
                                Body = podcast.Body,
                                BackgroundImageUrl = podcast.BackgroundImage?.Src,
                                MobileBackgroundImageUrl = podcast.MobileBackgroundImage?.Src,
                                BackgroundImageOpacity = podcast.BackgroundImageOpacity,
                                PodcastsLabel = podcast.PodcastsLabel,
                                PodcastLinks = podcast.PodcastLinks?.Select(x => new PodcastLink
                                {
                                    PodcastLinkUrl = x.Link?.Url,
                                    PodcastLinkGoal = x.LinkGoal.ToString(),
                                    PodcastLinkIcon = x.Icon?.Src
                                }),
                            };
                        }
                    }
                    catch
                    {
                    }
                }

                result.Add(articleResult);
            }

            return result;
        }

        public ArticleFacetsResponse GetArticleFilterFacets(Guid articleFilterFacetConfigId)
        {
            var filterFacetConfigItem = _contentRepository.GetItem<IArticleListingFacetsConfig>(new GetItemByIdOptions(articleFilterFacetConfigId));

            var listingArticleFacetsResponse = new ArticleFacetsResponse();
            if (filterFacetConfigItem == null
                    || filterFacetConfigItem.FundsFolder == null
                    || filterFacetConfigItem.CategoriesFolder == null
                    || filterFacetConfigItem.FundManagersFolder == null
                    || filterFacetConfigItem.FundTeamsFolder == null)
            {
                return null;
            }

            listingArticleFacetsResponse.Facets = new List<Facet>
            {
                new Facet
                {
                    Name = filterFacetConfigItem.FundsLabel,
                    Items = filterFacetConfigItem.FundsFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString("N"), Name = x.Name }),
                },                
                new Facet
                {
                    Name = filterFacetConfigItem.FundManagersLabel,
                    Items = filterFacetConfigItem.FundManagersFolder?.Children?.Where(x => x.IsFundManager)?.Select(x => new FacetItem { Identifier = x.Id.ToString("N"), Name = x.Name }),
                },
                new Facet
                {
                    Name = filterFacetConfigItem.FundTeamsLabel,
                    Items = filterFacetConfigItem.FundTeamsFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString("N"), Name = x.Name })
                },
                new Facet
                {
                    Name = filterFacetConfigItem.CategoriesLabel,
                    Items = filterFacetConfigItem.CategoriesFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString("N"), Name = x.Name }),
                },
            };

            return listingArticleFacetsResponse;
        }

        public ISearchResponse<ITaxonomyContentResult> GetArticleListingResponse(string database, string contentTypes, string funds, string categories, string fundManagers, string fundTeams, int? month, int? year, string searchTerm, string sortOrder, int page, int take = 21)
        {
            var fromYear = year ?? 2000;
            var fromMonth = month ?? 1;
            var toYear = year ?? DateTime.Now.Year + 1;
            var toMonth = month ?? 12;
            page = page - 1;

            var articleSearchRequest = new ArticleSearchRequest
            {
                DatabaseName = database,
                FromDate = new DateTime(fromYear, fromMonth, 1),
                ContentTypes = contentTypes?.Split('|'),
                Funds = funds?.Split('|'),
                Categories = categories?.Split('|'),
                FundManagers = fundManagers?.Split('|'),
                FundTeams = fundTeams?.Split('|'),
                SearchTerm = searchTerm,
                Skip = page * take,
                Take = take,
                ToDate = new DateTime(toYear, toMonth, DateTime.DaysInMonth(toYear, toMonth))
            };

            ContentSearchResults<ArticleSearchResultItem> contentSearchResults;
            if (sortOrder == "ASC")
            {
                contentSearchResults = this._articleContentSearchService.GetDatedTaxonomyRelatedArticles(articleSearchRequest, result => result.OrderBy(x => x.ArticleDate));
            }
            else if (sortOrder == "DESC")
            {
                contentSearchResults = this._articleContentSearchService.GetDatedTaxonomyRelatedArticles(articleSearchRequest, result => result.OrderByDescending(x => x.ArticleDate));
            }
            else
            {
                contentSearchResults = this._articleContentSearchService.GetDatedTaxonomyRelatedArticles(articleSearchRequest);
            }

            var articleSearchResponse = new SearchResponse<ITaxonomyContentResult>();
            if (contentSearchResults.TotalResults > 0)
            {
                articleSearchResponse.SearchResults = this.MapArticleResultHits(contentSearchResults.SearchResults);
                articleSearchResponse.StatusMessage = "Success";
                articleSearchResponse.StatusCode = 200;
                articleSearchResponse.TotalResults = contentSearchResults.TotalResults;
            }
            else
            {
                articleSearchResponse.StatusMessage = "No search results found";
                articleSearchResponse.StatusCode = 404;
            }

            return articleSearchResponse;
        }
    }
}