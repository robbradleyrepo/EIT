namespace LionTrust.Feature.Article.Controllers
{
    using Glass.Mapper.Sc.Web;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Article.Repositories;
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.Abstractions;
    using Sitecore.ContentSearch.Utilities;
    using Sitecore.Data;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class FundManagerInsightsController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly IArticleContentSearchService _contentSearchService;
        private readonly string _databaseName;

        public FundManagerInsightsController(IMvcContext context, IArticleContentSearchService contentSearchService, IRequestContext requestContext)
        {
            _context = context;
            _contentSearchService = contentSearchService;
            _databaseName = requestContext.SitecoreService.Database.Name;
        }

        public ActionResult Render()
        {
            FundManagerInsightsViewModel fundManagerInsightsViewModel = null;
            var data = _context.GetDataSourceItem<IFundManagerInsights>();
            IEnumerable<IArticlePromo> articles = new List<IArticlePromo>();

            if (data != null)
            {
                if (data.SelectedArticles != null && data.SelectedArticles.Any())
                {
                    articles = data.SelectedArticles.Where(x => OnboardingHelper.HasAccess(x.Fund?.ExcludedCountries))?.OrderByDescending(x => x.Date).Take(6);
                }
                else
                {
                    articles = new ArticleRepository(_contentSearchService, _context).Map(data, _databaseName);
                }

                articles = articles?.Where(x => x != null);
                fundManagerInsightsViewModel = new FundManagerInsightsViewModel(data, articles);

                var fundIds = string.Join("|", data.Funds?.Select(f => IdHelper.NormalizeGuid(f.Id, true)));
                var contentTypes = string.Join("|", data.ContentTypes?.Select(fc => fc.ArticleType.ToString("B")));
                var fundTeams = string.Join("|", data.FundTeams?.Select(ft => IdHelper.NormalizeGuid(ft.Id, true)));
                var fundManagers = string.Join("|", data.FundManagers?.Select(fm => IdHelper.NormalizeGuid(fm.Id, true)));
                var topics = string.Join("|", data.Topics?.Select(t => IdHelper.NormalizeGuid(t.Id, true)));

                var urlQuery = string.Empty;
                if (!string.IsNullOrEmpty(fundIds))
                {
                    urlQuery = $"ids={fundIds}";
                }

                if (!string.IsNullOrEmpty(contentTypes))
                {
                    urlQuery = string.IsNullOrEmpty(urlQuery) ? $"contentType={contentTypes}" : $"{urlQuery}&contentType={contentTypes}";
                }

                if (!string.IsNullOrEmpty(fundTeams))
                {
                    urlQuery = string.IsNullOrEmpty(urlQuery) ? $"fundTeamIds={fundTeams}" : $"{urlQuery}&fundTeamIds={fundTeams}";
                }

                if (!string.IsNullOrEmpty(fundManagers))
                {
                    urlQuery = string.IsNullOrEmpty(urlQuery) ? $"fundManagerIds={fundManagers}" : $"{urlQuery}&fundManagerIds={fundManagers}";
                }

                if (!string.IsNullOrEmpty(topics))
                {
                    urlQuery = string.IsNullOrEmpty(urlQuery) ? $"categoryIds={topics}" : $"{urlQuery}&categoryIds={topics}";
                }

                if (fundManagerInsightsViewModel.Data.CTA != null && !string.IsNullOrEmpty(urlQuery))
                {
                    fundManagerInsightsViewModel.Data.CTA.Query = urlQuery;
                }
            }

            return View("/views/article/fundmanagerinsights.cshtml", fundManagerInsightsViewModel);
        }
    }
}