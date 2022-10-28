using FuseIT.S4S.SitecoreSalesforceListBuilder.Models;
using FuseIT.S4S.SitecoreSalesforceListBuilder.Repositories;
using Glass.Mapper.Sc;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.ListManagement.Services.Model;
using Sitecore.ListManagement.Services.Repositories;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Sitecore.Services;
using System.Web.Http;
using System;
using System.Linq;
using LionTrust.Feature.EXM.Models;
using LionTrust.Foundation.Logging.Repositories;
using LionTrust.Feature.EXM.Repositories.Interfaces;
using System.Threading.Tasks;

namespace LionTrust.Feature.EXM.Controllers
{
    [Authorize]
    [ServicesController]
    public class SalesforceCampaignsController : EntityService<SalesforceCampaignEntity>
    {
        private readonly ISalesforceCampaignRepository _repository;
        private readonly ISitecoreService _sitecoreService;
        private readonly IFetchRepository<ContactListModel> _contactListRepository;
        private readonly ILogRepository _logRepository;

        public SalesforceCampaignsController(
          ISalesforceCampaignRepositoryActions<SalesforceCampaignEntity> repository,
          ISitecoreService sitecoreService,
          IFetchRepository<ContactListModel> contactListRepository,
          ISalesforceCampaignRepository salesforceCampaignRepository,
          ILogRepository logRepository)
          : base(repository)
        {
            _repository = salesforceCampaignRepository;
            _sitecoreService = sitecoreService;
            _contactListRepository = contactListRepository;
        }

        public SalesforceCampaignsController()
          : this(new SalesforceCampaignRepository(),
                new SitecoreService("master"),
                ServiceLocator.ServiceProvider.GetService<IFetchRepository<ContactListModel>>(),
                ServiceLocator.ServiceProvider.GetService<ISalesforceCampaignRepository>(),
                ServiceLocator.ServiceProvider.GetService<ILogRepository>())
        {
        }

        [ActionName("ImportSaleforceCampaignsToSitecore")]
        [HttpPost]
        public async Task<SalesforceCampaignEntity> ImportSaleforceCampaignsToSitecore(
          string id,
          SalesforceCampaignEntity info)
        {
            var campaignEntity = await _repository.ImportCampaignsToSitecore(id, info);
            var contactListModel = _contactListRepository.GetAll().FirstOrDefault(x => x.Name == info.CustomListName);

            if (campaignEntity.IsSuccess && contactListModel != null && !string.IsNullOrWhiteSpace(contactListModel.Id))
            {
                var contactList = _sitecoreService.GetItem<IMessageCampaign>(new Guid(contactListModel.Id));
                contactList.SalesforceCampaignId = info.CampaignIdString;

                try
                {
                    _sitecoreService.SaveItem(new SaveOptions(contactList));
                }
                catch(Exception ex)
                {
                    _logRepository.Error(ex.Message, ex);
                }
            }

            return campaignEntity;
        }
    }
}