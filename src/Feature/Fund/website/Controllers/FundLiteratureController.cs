namespace LionTrust.Feature.Fund.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Literature;
    using LionTrust.Feature.Fund.Models;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Mvc.Controllers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class FundLiteratureController: SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly IDocumentRepository _documentRepository;

        public FundLiteratureController(IMvcContext context, IDocumentRepository documentRepository)
        {
            _context = context;
            _documentRepository = documentRepository;
        }

        public ActionResult Render()
        {
            var datasource = _context.GetDataSourceItem<ILiterature>();
            var model = new LiteratureViewModel(datasource);
            model.Documents = ArrangeDocuments(datasource.Fund);
            return View("/views/fund/literature.cshtml", model);
        }

        private Dictionary<string, List<IDocument>> ArrangeDocuments(IFund fund)
        {
            var documents = _documentRepository.GetRelatedDocuments(fund).Where(d => d.DocumentLink != null);

            return documents
                .Where(d => d.DocumentTypes.Any())
                .SelectMany(d => d.DocumentTypes, (d, docType) => new { Name = docType.ItemName, Document = d })
                .GroupBy(d => d.Name)
                .ToDictionary(g => g.Key, g => g.Select(d => d.Document).ToList());            
        }
    }
}