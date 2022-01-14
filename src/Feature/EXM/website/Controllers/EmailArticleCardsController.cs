using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using LionTrust.Feature.EXM.ViewModels;
using Sitecore.Mvc.Controllers;
using LionTrust.Feature.EXM.Repositories.Interfaces;
using System.Linq;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailArticleCardsController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;
        private readonly IArticleRepository _articleRepository;

        public EmailArticleCardsController(IMvcContext context, IArticleRepository articleRepository)
        {
            _mvcContext = context;
            _articleRepository = articleRepository;
        }

        public ActionResult ArticleCards()
        {
            var articleCards = _mvcContext.GetDataSourceItem<IArticleCards>();

            var limitArticles = articleCards?.NumberListingArticles > 0 ? articleCards.NumberListingArticles : 3;
            var articles = _articleRepository.GetLatestArticles(limitArticles);

            var model = new ArticleCardsViewModel
            {
                ArticleCards = articleCards,
                Articles = articles?.ToList(),
            };

            return View("~/Views/EXM/ArticleCards.cshtml", model);
        }
    }
}