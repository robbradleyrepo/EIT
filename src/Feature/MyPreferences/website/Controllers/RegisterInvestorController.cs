namespace LionTrust.Feature.MyPreferences.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Feature.MyPreferences.Repositories;
    using LionTrust.Feature.MyPreferences.Services;
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Foundation.Contact.Services;
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Web.Mvc;
    using static LionTrust.Feature.MyPreferences.Constants;
    using static LionTrust.Foundation.Onboarding.Constants;

    public class RegisterInvestorController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly BaseLog _log;
        private readonly EmailPreferencesService _emailPreferencesService;

        public RegisterInvestorController(IMvcContext context, BaseLog log, IMailManager mailManager, IEmailPreferencesRepository editEmailPreferencesRepository)
        {
            _context = context;
            _log = log;
            _emailPreferencesService = new EmailPreferencesService(editEmailPreferencesRepository, mailManager);
        }

        [HttpGet]
        public ActionResult RegisterInvestor(Errors error = Errors.None, string email = "")
        {
            var data = _context.GetDataSourceItem<IRegisterInvestor>();
            var home = _context.GetHomeItem<IHome>();

            if (data == null || home == null)
            {
                return null;
            }

            var investorType = OnboardingHelper.GetInvestorType(home.OnboardingConfiguration, _log);
            var viewModel = new RegisterInvestorViewModel(data, investorType);

            if (error == Errors.UserExists)
            {
                var resendEmailPageUrl = $"/api/{Sitecore.Mvc.Configuration.MvcSettings.SitecoreRouteName}/{ControllerContext.RouteData.Values["controller"].ToString()}/ResendEmail";
                resendEmailPageUrl = string.Format("{0}?{1}={2}&{3}=false&{4}={5}", resendEmailPageUrl, QueryStringNames.EmailPreferencefParams.EmailQueryStringKey, email, QueryStringNames.EmailPreferencefParams.IsContactQueryStringKey, QueryStringNames.EmailPreferencefParams.DatasourceIdQueryStringKey, data.Id);

                var userExistsErrorMessage = data.UserExistsErrorLabel;
                userExistsErrorMessage = userExistsErrorMessage.Replace(SitecoreTokens.RegisterUserProcess.ResendEmailLinkToken, resendEmailPageUrl);
                viewModel.Error = userExistsErrorMessage;
            }

            return View("~/Views/MyPreferences/RegisterInvestor.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult Submit(RegisterInvestorSubmit registerInvestorSubmit)
        {
            var error = Errors.None;
            var data = _context.SitecoreService.GetItem<IRegisterInvestor>(registerInvestorSubmit.DatasourceId);

            if (data == null)
            {
                return null;
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var userExists = false;
                    var emailTemplate = registerInvestorSubmit.UKResident ? data.UKEmailTemplate : data.NonUKEmailTemplate;

                    if (registerInvestorSubmit.InvestorType == InvestorType.Private)
                    {
                        var company = (!string.IsNullOrEmpty(data.CompanyFieldDefaultValue)) ? data.CompanyFieldDefaultValue : "Self";

                        var nonProfUserViewModel = new NonProfessionalUser
                        {
                            FirstName = registerInvestorSubmit.FirstName,
                            LastName = registerInvestorSubmit.LastName,
                            Email = registerInvestorSubmit.Email,
                            IsUKResident = registerInvestorSubmit.UKResident,
                            Company = company
                        };

                        var savedUser = _emailPreferencesService.SaveNonProfUserAsSFLead(nonProfUserViewModel, emailTemplate, data.EditPreferencesPage.Url);

                        if (savedUser != null)
                        {
                            userExists = savedUser.IsUserExists;
                        }
                    }
                    else if (registerInvestorSubmit.InvestorType == InvestorType.Professional)
                    {
                        var sfOrganisationId = (!string.IsNullOrEmpty(data.DefaultSFOrganisationId)) ? data.DefaultSFOrganisationId : string.Empty;

                        var professionalUser = new ProfessionalUser
                        {
                            FirstName = registerInvestorSubmit.FirstName,
                            LastName = registerInvestorSubmit.LastName,
                            Email = registerInvestorSubmit.Email,
                            IsUKResident = registerInvestorSubmit.UKResident,
                            CompanyId = registerInvestorSubmit.CompanyId,
                            CompanyName = registerInvestorSubmit.CompanyName,
                            Organisation = sfOrganisationId
                        };

                        var savedUser = _emailPreferencesService.SaveProfUserAsSFContact(professionalUser, emailTemplate, data.EditPreferencesPage.Url);

                        if (savedUser != null)
                        {
                            userExists = savedUser.IsUserExists;
                        }
                    }

                    if (!userExists)
                    {
                        return Redirect(data.ConfirmationPage.Url);
                    }
                    else
                    {
                        error = Errors.UserExists;
                    }
                }
                else
                {
                    error = Errors.General;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Exception occured when registering new user as a SF Lead.", ex, this);
                error = Errors.General;
            }

            return Redirect($"{Request.RawUrl}?{QueryStringNames.EmailPreferencefParams.ErrorQueryStringKey}={(int)error}&{QueryStringNames.EmailPreferencefParams.EmailQueryStringKey}={registerInvestorSubmit.Email}");
        }

        public ActionResult ResendEmail(string email, bool isContact, Guid dataSourceId)
        {
            var data = _context.SitecoreService.GetItem<IRegisterInvestor>(dataSourceId);

            if (data == null)
            {
                return null;
            }

            var isSuccess = false;

            if (!string.IsNullOrEmpty(email))
            {
                isSuccess = _emailPreferencesService.ResendEditEmailPrefLink(email, isContact, data.ResendEditPreferencesEmailTemplate, data.EditPreferencesPage.Url);
            }

            if (isSuccess)
            {
                return Redirect(data.ResendEmailSuccessPage.Url);
            }
            else
            {
                return Redirect(data.ResendEmailFailedPage.Url);
            }
        }
    }
}