using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.EDS.Core.Net.Pop3;
using Sitecore.EDS.Core.Reporting;
using Sitecore.EDS.Providers.CustomSmtp.Reporting;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Modules.EmailCampaign;
using Sitecore.Modules.EmailCampaign.Services;
using System;
using System.Collections.Generic;

namespace LionTrust.Feature.EXM.Pipelines
{
    public class CustomManagerRootsPop3ReceiversCollection : IPop3ReceiversCollection
    {
        private readonly ILogger _logger;
        private readonly IManagerRootService _managerRootService;
        private readonly IBounceInspector _inspector;
        private readonly IEnvironmentId _environmentId;

        public CustomManagerRootsPop3ReceiversCollection(
          IBounceInspector inspector,
          IEnvironmentId environmentId,
          ILogger logger)
          : this(ServiceLocator.ServiceProvider.GetService<IManagerRootService>(), inspector, environmentId, logger)
        {
        }

        public CustomManagerRootsPop3ReceiversCollection(
          IManagerRootService managerRootService,
          IBounceInspector inspector,
          IEnvironmentId environmentId,
          ILogger logger)
        {
            Assert.ArgumentNotNull(managerRootService, nameof(managerRootService));
            Assert.ArgumentNotNull(inspector, nameof(inspector));
            Assert.ArgumentNotNull(environmentId, nameof(environmentId));
            Assert.ArgumentNotNull(logger, nameof(logger));
            _managerRootService = managerRootService;
            _inspector = inspector;
            _environmentId = environmentId;
            _logger = logger;
        }

        public IEnumerable<IPop3BounceReceiver> Receivers()
        {
            var pop3BounceReceiverList = new List<IPop3BounceReceiver>();
            foreach (ManagerRoot managerRoot in _managerRootService.GetManagerRoots())
            {
                if (managerRoot.Settings.GatherNotifications)
                {
                    var settings = new Pop3Settings()
                    {
                        Password = managerRoot.Settings.POP3Password,
                        Port = managerRoot.Settings.POP3Port,
                        Server = managerRoot.Settings.POP3Server,
                        UseSsl = managerRoot.Settings.POP3SSL,
                        UserName = managerRoot.Settings.POP3UserName,
                        StartTls = !managerRoot.Settings.POP3SSL
                    };

                    try
                    {
                        var receiver = CreateReceiver(settings);
                        pop3BounceReceiverList.Add(receiver);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex);
                    }
                }
            }
            return pop3BounceReceiverList;
        }

        private CustomChilkatPop3BounceReceiver CreateReceiver(Pop3Settings settings)
        {
            Assert.ArgumentNotNullOrEmpty(settings.Server, "settings.Server");
            Assert.ArgumentCondition(settings.Port > 0, "settings.Port", "Missing Port number.");
            return new CustomChilkatPop3BounceReceiver(settings, _inspector, _environmentId, _logger);
        }
    }
}
