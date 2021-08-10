namespace LionTrust.Feature.Article.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Glass.Mapper.Sc.Web;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Article.Repositories;
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.Mvc.Controllers;
    
    public class ArticleController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;
        private readonly IArticleContentSearchService _contentSearchService;
        private readonly string _databaseName;

        public ArticleController(IMvcContext context, IArticleContentSearchService contentSearchService, IRequestContext requestContext)
        {
            _mvcContext = context;
            _contentSearchService = contentSearchService;
            _databaseName = requestContext.SitecoreService.Database.Name;
        }

        public ActionResult ArticleScroller()
        {
            var articleScrollerViewModel = new ArticleScrollerViewModel();
            articleScrollerViewModel.ArticleScroller = _mvcContext.GetDataSourceItem<IArticleScroller>();

            if (!Sitecore.Context.PageMode.IsExperienceEditor && articleScrollerViewModel.ArticleScroller == null)
            {
                return null;
            }
            else if (HasSelectedArticles(articleScrollerViewModel))
            {
                articleScrollerViewModel.ArticleList = articleScrollerViewModel.ArticleScroller.SelectedArticles
                    .Where(a => OnboardingHelper.HasAccess(a.Fund.FundReference.ExcludedCountries))?
                    .OrderByDescending(a => a.Date);
            }
            else if (IsFilterSet(articleScrollerViewModel.ArticleScroller))
            {
                articleScrollerViewModel.ArticleList = new ArticleRepository(_contentSearchService, _mvcContext).Map(articleScrollerViewModel.ArticleScroller, _databaseName);
            }
            else
            {
                var currentPage = _mvcContext.GetPageContextItem<IArticle>();

                if (currentPage == null || currentPage.Topics == null || !currentPage.Topics.Any())
                {
                    return new EmptyResult();
                }

                var fundList = new List<Guid>();
                if (currentPage.Fund != null)
                {
                    fundList.Add(currentPage.Fund.Id);
                }

                var fundCategories = new List<Guid>();
                if (currentPage.PromoType != null)
                {
                    fundCategories.Add(currentPage.PromoType.Id);
                }

                articleScrollerViewModel.ArticleList =
                    new ArticleRepository(_contentSearchService, _mvcContext).Map(
                        fundList,
                        fundCategories,
                        null,
                        currentPage.Authors?.Select(a => a.Id),
                        currentPage.Topics?.Select(t => t.Id),
                        _databaseName);


                new ArticleRepository(_contentSearchService, _mvcContext)
                        .GetArticlePromosByTopics(currentPage.Topics.Select(x => x.Id));
            }

            if (!Sitecore.Context.PageMode.IsExperienceEditor && (articleScrollerViewModel.ArticleList == null || !articleScrollerViewModel.ArticleList.Any()))
            {
                return null;
            }

            return View("~/Views/Article/ArticleScroller.cshtml", articleScrollerViewModel);
        }

        public ActionResult ArticleLinks()
        {
            var articleLinksViewModel = new ArticleLinksViewModel();
            articleLinksViewModel.ArticleLinks = _mvcContext.GetDataSourceItem<IArticleLinks>();
            articleLinksViewModel.Article = _mvcContext.GetPageContextItem<IArticle>();

            return View("~/Views/Article/ArticleLinks.cshtml", articleLinksViewModel);
        }

        public ActionResult ArticleContent()
        {
            var articleDatasourceContent = _mvcContext.GetDataSourceItem<IArticleRichText>();
            if (articleDatasourceContent != null)
            {
                return View("~/Views/Article/ArticleRichText.cshtml", articleDatasourceContent);
            }
            else
            {
                var articlePage = _mvcContext.GetPageContextItem<IArticle>();
                return View("~/Views/Article/ArticleContent.cshtml", articlePage);
            }
        }

        private bool HasSelectedArticles(ArticleScrollerViewModel articleScrollerViewModel)
        {
            bool result = false;

            if (articleScrollerViewModel != null
                && articleScrollerViewModel.ArticleScroller?.SelectedArticles != null
                && articleScrollerViewModel.ArticleScroller.SelectedArticles.Any())
            {
                result = true;
            }

            return result;
        }

        private bool IsFilterSet(IArticleFilter filter)
        {
            var result = false;

            if(filter.ContentTypes != null && filter.ContentTypes.Any())
            {
                result = true;
            }
            else if(filter.FundManagers != null && filter.FundManagers.Any())
            {
                result = true;
            }
            else if(filter.Funds != null && filter.Funds.Any())
            {
                result = true;
            }
            else if(filter.FundTeams != null && filter.FundTeams.Any())
            {
                result = true;
            }
            else if (filter.Topics != null && filter.Topics.Any())
            {
                result = true;
            }

            return result;
        }
    }
}