using Microsoft.Extensions.DependencyInjection;
using Sitecore.EmailCampaign.Cm.Dispatch;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Modules.EmailCampaign.Core;
using Sitecore.Modules.EmailCampaign.Core.Contacts;
using Sitecore.EmailCampaign.Cm.Pipelines.DispatchNewsletter;
using Sitecore.DependencyInjection;
using LionTrust.Feature.EXM.Services.Interfaces;

namespace LionTrust.Feature.EXM.Pipelines
{
    public class CustomSendTestMessage : SendTestMessage
    {
        public CustomSendTestMessage(
          IContactService contactService,
          ILogger logger,
          PipelineHelper pipelineHelper,
          IMessageTaskRunner messageTaskRunner)
            : this (ServiceLocator.ServiceProvider.GetService<ICustomContactService>(), logger, pipelineHelper, messageTaskRunner)
        { }

        public CustomSendTestMessage(
          ICustomContactService contactService,
          ILogger logger,
          PipelineHelper pipelineHelper,
          IMessageTaskRunner messageTaskRunner)
            : base(contactService, logger, pipelineHelper, messageTaskRunner)
        { }
    }
}
