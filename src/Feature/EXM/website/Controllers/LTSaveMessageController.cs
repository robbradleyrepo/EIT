using Glass.Mapper.Sc;
using LionTrust.Feature.EXM.Models;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.EmailCampaign.Server.Responses;
using Sitecore.EmailCampaign.Server.Services.Interfaces;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Modules.EmailCampaign.Core;
using Sitecore.Modules.EmailCampaign.Core.Data;
using Sitecore.Modules.EmailCampaign.Services;
using Sitecore.Services.Core;
using System;
using System.Web.Http;
using Sitecore.EmailCampaign.Server.Controllers.SaveMessage;

namespace LionTrust.Feature.EXM.Controllers
{
    [ServicesController]
    public class LTSaveMessageController : SaveMessageController
    {
        private readonly ISitecoreService _sitecoreService;

        public LTSaveMessageController()
            : this(
                  ServiceLocator.ServiceProvider.GetService<IExmCampaignService>(),
                  ServiceLocator.ServiceProvider.GetService<EcmDataProvider>(),
                  ServiceLocator.ServiceProvider.GetService<ItemUtilExt>(),
                  ServiceLocator.ServiceProvider.GetService<IMessageVariantsService>(),
                  ServiceLocator.ServiceProvider.GetService<ILogger>(),
                  new SitecoreService("master"))
        {
        }

        public LTSaveMessageController(
            IExmCampaignService exmCampaignService,
            EcmDataProvider dataProvider,
            ItemUtilExt util,
            IMessageVariantsService messageVariantsService,
            ILogger logger,
            ISitecoreService sitecoreService)
            : base(exmCampaignService, dataProvider, util, messageVariantsService, logger)
        {
            _sitecoreService = sitecoreService;
        }


        [ActionName("SaveMessage")]
        public Response LTSaveMessage(LTUpdateMessageContext data)
        {
            Response save = base.SaveMessage(data);

            if (!save.Error)
            {
                var message = _sitecoreService.GetItem<IMessageCampaign>(new Guid(data.Message.Id));

                if (!string.IsNullOrEmpty(data.Team))
                {
                    message.Team = new Guid(data.Team);
                }
                else
                {
                    message.Team = null;
                }

                message.KeepDefaultSender = data.KeepDefaultSender;
                
                _sitecoreService.SaveItem(new SaveOptions(message));
            }

            return save;
        }
    }
}