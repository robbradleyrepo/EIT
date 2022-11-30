namespace LionTrust.Feature.Fund.Api.Error
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Abstractions;
    using Sitecore.DependencyInjection;
    using Sitecore.Diagnostics;
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Class to track errors and send reduced email notifications
    /// </summary>
    [Service(ServiceType = typeof(IConditionalErrorTracker), Lifetime = Lifetime.Singleton)]
    public class ConditionalErrorTracker : IConditionalErrorTracker
    {
        private readonly IMailManager _mailManager;
        private readonly double _suspendErrorEmailsIntervalInHours;
        private DateTime? _lastError;
        private DateTime? _lastSuccess;
        private DateTime? _lastEmailFailure;
        private DateTime? _lastEmailSuccess;
        private object _locker = new object();

        public ConditionalErrorTracker(BaseSettings settings, IMailManager mailManager)
        {
            _mailManager = mailManager;

            if (double.TryParse(settings.GetSetting(Constants.Settings.SuspendErrorEmailsIntervalInHours), out var suspendErrorEmailsIntervalInHours))
            {
                this._suspendErrorEmailsIntervalInHours = suspendErrorEmailsIntervalInHours;
            }
        }

        public void Error(string message, object owner, bool trySendEmail = true)
        {
            _lastError = DateTime.UtcNow;
            Log.Error(message, owner);

            if (trySendEmail)
            {
                TrySendEmail(message, false);
            }
        }


        public void TrySendEmail(string message, bool isSuccess = false)
        {
            if (ShouldSuspendEmail(isSuccess))
            {
                if (!isSuccess)
                {
                    Log.Info($"Email send suspended. Message: {message}", this);
                }
                return;
            }

            lock (_locker)
            {
                if (ShouldSuspendEmail(isSuccess))
                {
                    if (!isSuccess)
                    {
                        Log.Info($"Email send suspended. Message: {message}", this);
                    }
                    return;
                }

                try
                {
                    // HACK: this is a singleton so IMvcContext can not be passed in constructor
                    IMvcContext context = ServiceLocator.ServiceProvider.GetService<IMvcContext>();
                    var emailTemplate = context.SitecoreService.GetItem<IEmail>(Constants.EmailTemplates.FundImportErrorEmail);
                    if (emailTemplate == null)
                    {
                        Log.Error("[ExternalFund]: No error email template found", this);
                        return;
                    }

                    string subject = isSuccess ? $"[FIXED] {emailTemplate.Subject}" : emailTemplate.Subject;

                    _mailManager.SendEmail(emailTemplate.FromAddress, emailTemplate.FromDisplayName, emailTemplate.ToAddresses, subject, message, true);

                    if (isSuccess)
                    {
                        _lastEmailSuccess = DateTime.UtcNow;
                    }
                    else
                    {
                        _lastEmailFailure = DateTime.UtcNow;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("[ExternalFund]: Error sending email with Fund API information", ex, this);
                }
            }
        }

        private bool ShouldSuspendEmail(bool isSuccess)
        {
            if (isSuccess)
            {
                return !_lastEmailFailure.HasValue || //no failure
                    (_lastEmailSuccess.HasValue && _lastEmailSuccess.Value > _lastEmailFailure.Value); //no failure after last success
            }

            return _lastEmailFailure.HasValue &&    //there was a failure
                (!_lastEmailSuccess.HasValue || _lastEmailFailure.Value > _lastEmailSuccess.Value) &&   //no success after last failure
                _lastEmailFailure.Value.AddHours(_suspendErrorEmailsIntervalInHours) > DateTime.UtcNow; //not in suspension time frame
        }

        public void Success(string message = "", bool trySendEmail = true)
        {
            _lastSuccess = DateTime.UtcNow;

            if (trySendEmail)
            {
                TrySendEmail(message, true);
            }
        }
    }
}