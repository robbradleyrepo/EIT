using LionTrust.Foundation.Search.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.ListManagement.Services;
using Sitecore.ListManagement.Services.Model;
using Sitecore.Services.Infrastructure.Web.Http;
using System.Collections.Generic;
using System.Web.Http;

namespace LionTrust.Feature.EXM.Controllers
{
    [AnalyticsDisabledFilter]
    [AccessDeniedExceptionFilter]
    [UnauthorizedAccessExceptionFilter]
    [SitecoreAuthorize(Roles = "sitecore\\List Manager Editors")]
    public class LTListController : ServicesApiController
    {
        private readonly IContactListSearchRepository _contactListSearchRepository;

        public LTListController()
            : this(ServiceLocator.ServiceProvider.GetService<IContactListSearchRepository>())
        { }

        public LTListController(IContactListSearchRepository contactListSearchRepository)
        {
            _contactListSearchRepository = contactListSearchRepository;
        }

        [Route("sitecore/api/ssc/ListManagement/LTList")]
        [HttpGet]
        public FetchResult<ListModel> GetAllActiveLists()
        {
            var results = _contactListSearchRepository.GetAllActiveContactListSearchResultItems();

            var list = new List<ListModel>();
            foreach(var item in results.SearchResults)
            {
                var model = new ListModel
                {
                    Created = item.Document.CreatedDate.ToLocalTime().ToString("yyyyMMddTHHmmss"),
                    CreatedBy = item.Document.CreatedBy,
                    Id = item.Document.ItemId.Guid.ToString("D"),
                    Name = item.Document.Name,
                    Type = item.Document.Type,
                    TypeName = item.Document.TypeName,
                    Updated = item.Document.Updated.ToLocalTime().ToString("yyyyMMddTHHmmss")
                };

                list.Add(model);
            }

            return new FetchResult<ListModel>(list, results.TotalResults);
        }
    }
}