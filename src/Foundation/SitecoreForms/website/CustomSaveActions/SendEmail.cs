namespace Liontrust.Foundation.SitecoreForms.CustomSaveActions
{
    using System;
    using System.Collections.Generic;
    
    using LionTrust.Foundation.SitecoreForms.Models;
    using LionTrust.Foundation.SitecoreForms.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;
    using Sitecore.Diagnostics;
    using Sitecore.ExperienceForms.Models;
    using Sitecore.ExperienceForms.Mvc.Models.Fields;
    using Sitecore.ExperienceForms.Processing;
    using Sitecore.ExperienceForms.Processing.Actions;

    /// <summary>
    /// Sitecore forms custom save action for email sending
    /// </summary>
    public class SendEmail : SubmitActionBase<SendEmailActionData>
    {
        private readonly ISitecoreFormsCustomSaveActionsService _customSaveActionService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="submitActionData"></param>
        public SendEmail(ISubmitActionData submitActionData) : base(submitActionData)
        {
            _customSaveActionService = ServiceLocator.ServiceProvider.GetService<ISitecoreFormsCustomSaveActionsService>();
        }

        /// <summary>
        /// Send email custom save action functionalities
        /// </summary>
        /// <param name="data"></param>
        /// <param name="formSubmitContext"></param>
        /// <returns></returns>
        protected override bool Execute(SendEmailActionData data, FormSubmitContext formSubmitContext)
        {
            Log.Debug(string.Format("Executing SendEmail custom save action for form id: {0}.", formSubmitContext.FormId.ToString()), this);

            Assert.ArgumentNotNull(formSubmitContext, nameof(formSubmitContext));
            if (data == null || data.ReferenceId == Guid.Empty)
            {
                Log.Debug(string.Format("ReferenceId null or empty. SendEmail submit action failed for form id: {0}.", formSubmitContext.FormId.ToString()), this);
                return false;
            }

            try
            {
                var emailTemplateForSendEmailSaveAction = _customSaveActionService.GetEmailTemplate(data.ReferenceId);

                //Replace keywords in 'Subject' from form fields
                var emailSubject = ReplaceKeywords(emailTemplateForSendEmailSaveAction.Subject, formSubmitContext);

                //Replace 'From' email address from form fields
                var fromEmailAddress = ReplaceKeywords(emailTemplateForSendEmailSaveAction.From, formSubmitContext);

                //Replace keywords in 'FromDisplayName' from form fields
                var fromDisplayName = ReplaceKeywords(emailTemplateForSendEmailSaveAction.FromDisplyName, formSubmitContext);

                //Replace 'TO' email addresses from form fields                
                var toEmailAddresses = ReplaceKeywords(emailTemplateForSendEmailSaveAction.To, formSubmitContext);

                //Replace 'CC' email addresses from form fields                
                var ccEmailAddresses = ReplaceKeywords(emailTemplateForSendEmailSaveAction.CC, formSubmitContext);

                //Replace 'BCC' email addresses from form fields                
                var bccEmailAddresses = ReplaceKeywords(emailTemplateForSendEmailSaveAction.BCC, formSubmitContext);

                //Replace email message body from form fields
                var message = ReplaceKeywords(emailTemplateForSendEmailSaveAction.Message, formSubmitContext);

                _customSaveActionService.SendEmailAsCustomSaveAction(fromEmailAddress, fromDisplayName, toEmailAddresses, ccEmailAddresses, bccEmailAddresses, emailSubject, message, true);
                Log.Debug(string.Format("Email sent with following details for form id: {0}: Subject- {1} | FromAddress - {2} | FromDisplyName - {3} | ToAddress - {4} | CCAddress - {5} | BCCAddress - {6}",
                    formSubmitContext.FormId.ToString(), emailSubject, fromEmailAddress, fromDisplayName, toEmailAddresses, ccEmailAddresses, bccEmailAddresses), this);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Exception occured executing send email custom save action for form id: {0}.", formSubmitContext.FormId.ToString()), ex, this);
                return false;
            }
        }

        /// <summary>
        /// Replace keywords from the form input data
        /// </summary>
        /// <param name="original"></param>
        /// <param name="formSubmitContext"></param>
        /// <returns></returns>
        protected string ReplaceKeywords(string original, FormSubmitContext formSubmitContext)
        {
            var returnString = original;
            foreach (var viewModel in formSubmitContext.Fields)
            {
                if (returnString.Contains("{" + viewModel.Name + "}"))
                {
                    var type = viewModel.GetType();
                    string valueToReplace = string.Empty;

                    // InputViewModel<string> types
                    if (type.IsSubclassOf(typeof(InputViewModel<string>)))
                    {
                        var field = (InputViewModel<string>)viewModel;
                        valueToReplace = field.Value ?? string.Empty; ;
                    }
                    // InputViewModel<List<string>> types
                    else if (type.IsSubclassOf(typeof(InputViewModel<List<string>>)))
                    {
                        var field = (InputViewModel<List<string>>)viewModel;
                        valueToReplace = (field.Value != null) ? string.Join(", ", field.Value) : string.Empty;
                    }
                    // InputViewModel<bool> types
                    else if (type.IsSubclassOf(typeof(InputViewModel<bool>)))
                    {
                        var field = (InputViewModel<bool>)viewModel;
                        valueToReplace = field.Value.ToString();
                    }
                    // InputViewModel<DateTime?> types
                    else if (type.IsSubclassOf(typeof(InputViewModel<DateTime?>)))
                    {
                        var field = (InputViewModel<DateTime?>)viewModel;
                        valueToReplace = field.Value?.ToString() ?? string.Empty;
                    }
                    // InputViewModel<DateTime> types
                    else if (type.IsSubclassOf(typeof(InputViewModel<DateTime>)))
                    {
                        var field = (InputViewModel<DateTime>)viewModel;
                        valueToReplace = field.Value.ToString();
                    }
                    // InputViewModel<double?> types
                    else if (type.IsSubclassOf(typeof(InputViewModel<double?>)))
                    {
                        var field = (InputViewModel<double?>)viewModel;
                        valueToReplace = field.Value?.ToString() ?? string.Empty;
                    }

                    returnString = returnString.Replace("{" + viewModel.Name + "}", valueToReplace);
                }
            }

            return returnString;
        }
    }
}
