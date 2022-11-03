namespace LionTrust.Feature.MyPreferences.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Feature.MyPreferences.Services;
    using LionTrust.Foundation.Analytics.Goals;
    using LionTrust.Foundation.Contact.Services;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Web;
    using System.Web.Mvc;
    using static LionTrust.Feature.MyPreferences.Constants;
    using static LionTrust.Foundation.Contact.Constants;

    public class EditPreferencesController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly BaseLog _log;
        private readonly IEmailPreferencesService _emailPreferencesService;
        private readonly IPersonalizedContentService _personalizedContentService;

        public EditPreferencesController(IMvcContext context, BaseLog log, IPersonalizedContentService personalizedContentService, IEmailPreferencesService emailPreferencesService)
        {
            _context = context;
            _log = log;
            _personalizedContentService = personalizedContentService;
            _emailPreferencesService = emailPreferencesService;
        }

        public ActionResult Render(Errors error = Errors.None)
        {
            var data = _context.GetDataSourceItem<IEditEmailPreferences>();
            
            //remove context if user opens email link an existing session
            _personalizedContentService.UpdateContext(null);
            var context = _personalizedContentService.GetContext();

            if (data == null || context == null)
            {
                return null;
            }

            var viewModel = new EditEmailPreferencesViewModel(context, data);

            if (context.Preferences == null || error == Errors.General)
            {
                viewModel.Error = data.GenericError;
            }

            return View("~/Views/MyPreferences/EditEmailPreferences.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult Render(EditEmailPreferencesViewModel registerInvestorViewModel)
        {
            var submitSuccess = true;
            Guid subscriptionGoal = Guid.Empty;

            if(registerInvestorViewModel == null || registerInvestorViewModel.DatasourceId == null)
            {
                return null;
            }

            var data = _context.SitecoreService.GetItem<IEditEmailPreferences>(registerInvestorViewModel.DatasourceId);

            if (data == null)
            {
                return null;
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var context = _personalizedContentService.GetContext();

                    if(registerInvestorViewModel.UnsubscribeAll)
                    {
                        context.Preferences.UnsubscribeAll();
                        subscriptionGoal = data.UnsubscribeGoal;
                    }   
                    else
                    {
                        context.Preferences.SubscribeAll();
                        subscriptionGoal = data.SubscribeGoal;
                    }

                    context.Preferences.TortoiseNewsletter = registerInvestorViewModel.ShowUnsubscribeTortoise && !registerInvestorViewModel.UnsubscribeTortoise;
                    context.Preferences.SFProcessList = registerInvestorViewModel.SFProcessList;
                    submitSuccess = _emailPreferencesService.SaveEmailPreferences(context);
                }
                else
                {
                    _log.Debug("No correct format Salesforce Entity Id or RandomGuid found.", this);
                    submitSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Exception occured when executing button click event for save email preferences.", ex, this);
                submitSuccess = false;
            }

            var redirectUrl = data.SuccessPage.Url;

            if (!submitSuccess)
            {
                var uriBuilder = new UriBuilder(Request.Url.ToString());
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query.Add(QueryStringNames.EmailPreferencefParams.ErrorQueryStringKey, Errors.General.ToString());
                uriBuilder.Query = query.ToString();
                
                redirectUrl = uriBuilder.Uri.PathAndQuery;
            }

            // trigger subscription goal
            if (subscriptionGoal != Guid.Empty)
            {
                Helper.TriggerGoal(new Sitecore.Data.ID(subscriptionGoal));
            }
            
            return Redirect(redirectUrl);
        }
    }
}