namespace LionTrust.Feature.Listings.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Listings.Models;
    using LionTrust.Foundation.Content.Repositories;
    using LionTrust.Foundation.LocalDatasource;
    using Sitecore.Mvc.Controllers;
   
    public class LatestResultsAndPresentationsController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;
        private readonly IRenderingRepository _repository;

        public LatestResultsAndPresentationsController(IMvcContext mvcContext, IRenderingRepository repository)
        {
            _mvcContext = mvcContext;
            _repository = repository;
        }

        public ActionResult Render()
        {
            var viewModel = new LatestResultsAndPresentationsViewModel();
            viewModel.Data = _repository.GetDataSourceItem<ILatestResultsAndPresentations>();

            if (viewModel.Data == null)
            {
                return null;
            }

            if (viewModel.Data.AllResultsPage != null 
                && viewModel.Data.AllResultsPage.Children != null
                && viewModel.Data.AllResultsPage.Children.Any())
            {
                var localContentFolder = viewModel.Data.AllResultsPage.Children.Where(x => x.TemplateId.Equals(Settings.LocalDatasourceFolderTemplate))?.FirstOrDefault();
                if (localContentFolder != null
                    && localContentFolder.Children != null
                    && localContentFolder.Children.Any())
                {
                    var documentListerVariantTemplateId = new Guid(Constants.DocumentListerVariant.TemplateId);
                    var component = localContentFolder.Children.Where(x => x.BaseTemplateIds.Contains(documentListerVariantTemplateId))?.FirstOrDefault();
                    if (component != null)
                    {
                        var documentListerVariantComponent = _mvcContext.SitecoreService.GetItem<IDocumentListerVariants>(component.Id);
                        if (documentListerVariantComponent != null 
                            && documentListerVariantComponent.DocumentVariants != null 
                            && documentListerVariantComponent.DocumentVariants.Any()) 
                        {
                            var latestDocumentVariants = documentListerVariantComponent.DocumentVariants
                                                            .OrderByDescending(x => x.Date != DateTime.MinValue ? x.Date : x.Created);
                            if (latestDocumentVariants.Count() > 3)
                            {
                                viewModel.LatestDocuments = latestDocumentVariants.Take(3);
                            }
                            else
                            {
                                viewModel.LatestDocuments = latestDocumentVariants;
                            }
                        }
                    }
                }
            }

            return View("~/Views/Listings/LatestResultsAndPresentations.cshtml", viewModel);
        }
    }
}