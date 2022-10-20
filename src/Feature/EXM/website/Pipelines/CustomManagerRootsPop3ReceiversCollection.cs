using Sitecore.Diagnostics;
using Sitecore.EDS.Core.Net.Pop3;
using Sitecore.EDS.Core.Reporting;
using Sitecore.EDS.Providers.CustomSmtp.Reporting;
using Sitecore.ExM.Framework.Diagnostics;
using System;
using System.Collections.Generic;

namespace LionTrust.Feature.EXM.Pipelines
{
    public class CustomManagerRootsPop3ReceiversCollection : IPop3ReceiversCollection
    {
        private readonly object syncLock = new object();
        private readonly IList<Pop3Settings> pop3Settings = new List<Pop3Settings>();
        private readonly IBounceInspector _inspector;
        private readonly IEnvironmentId _environmentId;
        private readonly ILogger _logger;

        public CustomManagerRootsPop3ReceiversCollection(
          IBounceInspector inspector,
          IEnvironmentId environmentId,
          ILogger logger)
        {
            Assert.ArgumentNotNull(inspector, nameof(inspector));
            Assert.ArgumentNotNull(environmentId, nameof(environmentId));
            Assert.ArgumentNotNull(logger, nameof(logger));

            _inspector = inspector;
            _environmentId = environmentId;
            _logger = logger;
        }

        public IEnumerable<IPop3BounceReceiver> Receivers()
        {
            var pop3BounceReceiverList = new List<IPop3BounceReceiver>();

            foreach (var settings in pop3Settings)
            {
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
            return pop3BounceReceiverList;
        }

        public void AddSettings(Pop3Settings settings)
        {
            Assert.ArgumentNotNull((object)settings, nameof(settings));

            lock (syncLock)
            {
                pop3Settings.Add(settings);
            }
        }

        private CustomChilkatPop3BounceReceiver CreateReceiver(Pop3Settings settings)
        {
            Assert.ArgumentNotNullOrEmpty(settings.Server, "settings.Server");
            Assert.ArgumentCondition(settings.Port > 0, "settings.Port", "Missing Port number.");
            return new CustomChilkatPop3BounceReceiver(settings, _inspector, _environmentId, _logger);
        }
    }
}
