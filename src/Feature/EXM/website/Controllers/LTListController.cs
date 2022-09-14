using LionTrust.Foundation.Search.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.ListManagement.Services;
using Sitecore.ListManagement.Services.Model;
using Sitecore.Services.Infrastructure.Web.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<FetchResult<ListModel>> GetAllActiveLists()
        {
            var results = _contactListSearchRepository.GetAllActiveContactListSearchResultItems();

            var list = new List<ListModel>();
            foreach(var item in results.SearchResults)
            {
                var model = _contactListSearchRepository.GetListModel(item.Document);
                list.Add(model);
            }

            return new FetchResult<ListModel>(list, results.TotalResults);
        }
    }
}