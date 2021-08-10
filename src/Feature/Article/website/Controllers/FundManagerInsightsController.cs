namespace LionTrust.Feature.Article.Controllers
{
    using Glass.Mapper.Sc.Web;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Article.Repositories;
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
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
            var data = _context.GetDataSourceItem<IFundManagerInsights>();
            IEnumerable<IArticlePromo> articles = new List<IArticlePromo>();

            if (!Sitecore.Context.PageMode.IsExperienceEditor && data == null)
            {
                return null;
            }
            else if (data.SelectedArticles != null && data.SelectedArticles.Any())
            {
                articles = data.SelectedArticles.Where(x => OnboardingHelper.HasAccess(x.Fund?.FundReference?.ExcludedCountries))?.OrderByDescending(x => x.Date).Take(6);
            }
            else
            {
                articles = new ArticleRepository(_contentSearchService, _context).Map(data, _databaseName);
            }

            var fundManagerInsightsViewModel = new FundManagerInsightsViewModel(data, articles);

            return View("/views/article/fundmanagerinsights.cshtml", fundManagerInsightsViewModel);
        }
    }
}