using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Services.Core;
using System.Web.Http;
using LionTrust.Feature.EXM.Models;
using Sitecore.Data.Items;
using Sitecore.EmailCampaign.Server.Contexts;
using Sitecore.EmailCampaign.Server.Responses;
using Sitecore.EmailCampaign.Server.Services.Interfaces;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Modules.EmailCampaign.Core;
using Sitecore.Modules.EmailCampaign.Messages.Interfaces;
using Sitecore.Modules.EmailCampaign.Services;
using Glass.Mapper.Sc;
using System;
using Sitecore.EmailCampaign.Server.Controllers.Message;

namespace LionTrust.Feature.EXM.Controllers
{
    [ServicesController]
    public class LTMessageController : MessageController
    {
        private readonly ISitecoreService _sitecoreService;

        public LTMessageController()
            : this(
                  ServiceLocator.ServiceProvider.GetService<ItemUtilExt>(),
                  ServiceLocator.ServiceProvider.GetService<ILanguageRepository>(),
                  ServiceLocator.ServiceProvider.GetService<IMessageVariantsService>(),
                  ServiceLocator.ServiceProvider.GetService<ILogger>(),
                  ServiceLocator.ServiceProvider.GetService<IExmCampaignService>(),
                  ServiceLocator.ServiceProvider.GetService<ISitecoreService>())
        {
        }

        public LTMessageController(
            ItemUtilExt itemUtil,
            ILanguageRepository languageRepository,
            IMessageVariantsService messageVariantsService,
            ILogger logger,
            IExmCampaignService exmCampaignService,
            ISitecoreService sitecoreService)
            : base(itemUtil, languageRepository, messageVariantsService, logger, exmCampaignService)
        {
            _sitecoreService = sitecoreService;
        }


        [ActionName("Message")]
        public Response LTMessage(MessageContext data)
        {
            var baseResponse = (MessageResponse)base.Message(data);
            var response = new LTMessageResponse(baseResponse);

            var message = _sitecoreService.GetItem<IMessageCampaign>(new Guid(data.MessageId));

            if (message.Team != null)
            {
                response.Message.Team = message.Team?.ToString();
                var team = _sitecoreService.GetItem<Item>(message.Team.Value);
                response.Message.TeamPath =  team.Paths.FullPath.Replace(team.Parent.Paths.FullPath, string.Empty);
            }

            return response;
        }
    }
}