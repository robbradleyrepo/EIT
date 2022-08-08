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

namespace LionTrust.Feature.EXM.Controllers
{
    [Authorize]
    [ServicesController]
    public class SalesforceCampaignsController : EntityService<SalesforceCampaignEntity>
    {
        private ISalesforceCampaignRepositoryActions<SalesforceCampaignEntity> _repository;
        private readonly ISitecoreService _sitecoreService;
        private readonly IFetchRepository<ContactListModel> _contactListRepository;

        public SalesforceCampaignsController(
          ISalesforceCampaignRepositoryActions<SalesforceCampaignEntity> repository,
          ISitecoreService sitecoreService,
          IFetchRepository<ContactListModel> contactListRepository)
          : base(repository)
        {
            _repository = repository;
            _sitecoreService = sitecoreService;
            _contactListRepository = contactListRepository;
        }

        public SalesforceCampaignsController()
          : this(new SalesforceCampaignRepository(),
                ServiceLocator.ServiceProvider.GetService<ISitecoreService>(),
                ServiceLocator.ServiceProvider.GetService<IFetchRepository<ContactListModel>>())
        {
        }

        [ActionName("ImportSaleforceCampaignsToSitecore")]
        [HttpPost]
        public SalesforceCampaignEntity ImportSaleforceCampaignsToSitecore(
          string id,
          SalesforceCampaignEntity info)
        {
            var campaignEntity = _repository.ImportCampaignsToSitecore(id, info);

            ContactListModel contactListModel = _contactListRepository.GetAll().FirstOrDefault(x => x.Name == info.CustomListName);

            if (campaignEntity.IsSuccess && contactListModel?.Id != null && !string.IsNullOrWhiteSpace(contactListModel.Id))
            {
                if (info.SelectedListMergeOption == "createnewlist")
                {
                    var contactList = _sitecoreService.GetItem<ICampaign>(new Guid(contactListModel.Id));
                    contactList.SalesforceCampaignId = info.CampaignIdString;

                    _sitecoreService.SaveItem(new SaveOptions(contactList));
                }
                else if (info.SelectedListMergeOption == "updatelist")
                {
                    var contactList = _sitecoreService.GetItem<ICampaign>(new Guid(contactListModel.Id));
                    contactList.SalesforceCampaignId = info.CampaignIdString;

                    _sitecoreService.SaveItem(new SaveOptions(contactList));
                }
            }

            return campaignEntity;
        }
    }
}