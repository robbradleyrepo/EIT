namespace LionTrust.Feature.MyPreferences.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Feature.MyPreferences.Repositories;
    using LionTrust.Feature.MyPreferences.Services;
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Foundation.Contact.Services;
    using Sitecore.Abstractions;
    using Sitecore.Annotations;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Web.Mvc;
    using static LionTrust.Foundation.Onboarding.Constants;

    public class EditEmailPreferencesController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly BaseLog _log;
        private readonly EmailPreferencesService _emailPreferencesService;

        public EditEmailPreferencesController(IMvcContext context, BaseLog log, IMailManager mailManager, IEmailPreferencesRepository editEmailPreferencesRepository)
        {
            _context = context;
            _log = log;
            _emailPreferencesService = new EmailPreferencesService(editEmailPreferencesRepository, mailManager);
        }

        public ActionResult Render(string @ref)
        {
            var data = _context.GetDataSourceItem<IRegisterInvestor>();
            
            if (data == null)
            {
                return null;
            }

            var sfEntityId = string.Empty;
            var sfRandomGUID = string.Empty;

            //Query string "ref" should have the format as follows: {GUID}_{entityId}
            if (!string.IsNullOrEmpty(@ref))
            {
                var queryStringParts = @ref.Split('_');
                if (queryStringParts.Length >= 2)
                {
                    sfRandomGUID = queryStringParts[0];
                    sfEntityId = queryStringParts[1];
                }
            }

            if (!string.IsNullOrEmpty(sfEntityId) && (sfEntityId.StartsWith("003", StringComparison.CurrentCultureIgnoreCase) || sfEntityId.StartsWith("00Q", StringComparison.CurrentCultureIgnoreCase)) && !string.IsNullOrEmpty(sfRandomGUID))
            {
                var isContact = (sfEntityId.StartsWith("003", StringComparison.CurrentCultureIgnoreCase)) ? true : false;
                var emailPreferences = _emailPreferencesService.GetEmailPreferences(sfEntityId, sfRandomGUID, isContact);

                var viewModel = new EditEmailPreferencesViewModel(emailPreferences);

                return View("~/Views/MyPreferences/EditEmailPreferences.cshtml", viewModel);
            }

            return null;
        }

        [HttpPost]
        public ActionResult Render([NotNull] RegisterInvestorViewModel registerInvestorViewModel)
        {
            try
            {
                var data = _context.GetDataSourceItem<IRegisterInvestor>();

                if (data == null)
                {
                    return null;
                }
                else
                {
                    registerInvestorViewModel.Content = data;
                }

                if (ModelState.IsValid)
                {
                    var userExists = false;
                    var emailTemplate = registerInvestorViewModel.UKResident ? data.UKEmailTemplate : data.NonUKEmailTemplate;

                    if (registerInvestorViewModel.InvestorType == InvestorType.Private)
                    {
                        var company = (!string.IsNullOrEmpty(registerInvestorViewModel.Content.CompanyFieldDefaultValue)) ? registerInvestorViewModel.Content.CompanyFieldDefaultValue : "Self";

                        var nonProfUserViewModel = new NonProfessionalUser
                        {
                            FirstName = registerInvestorViewModel.FirstName,
                            LastName = registerInvestorViewModel.LastName,
                            Email = registerInvestorViewModel.Email,
                            IsUKResident = registerInvestorViewModel.UKResident,
                            Company = company
                        };

                        var savedUser = _emailPreferencesService.SaveNonProfUserAsSFLead(nonProfUserViewModel, emailTemplate, data.EditPreferencesPage.Url);

                        if (savedUser != null)
                        {
                            userExists = savedUser.IsUserExists;
                        }
                    }
                    else if (registerInvestorViewModel.InvestorType == InvestorType.Professional)
                    {
                        var sfOrganisationId = (!string.IsNullOrEmpty(registerInvestorViewModel.Content.DefaultSFOrganisationId)) ? registerInvestorViewModel.Content.DefaultSFOrganisationId : string.Empty;

                        var profUserViewModel = new ProfessionalUser
                        {
                            FirstName = registerInvestorViewModel.FirstName,
                            LastName = registerInvestorViewModel.LastName,
                            Email = registerInvestorViewModel.Email,
                            IsUKResident = registerInvestorViewModel.UKResident,
                            CompanyId = registerInvestorViewModel.CompanyId,
                            CompanyName = registerInvestorViewModel.CompanyName,
                            Organisation = sfOrganisationId
                        };

                        var savedUser = _emailPreferencesService.SaveProfUserAsSFContact(profUserViewModel, emailTemplate, data.EditPreferencesPage.Url);

                        if (savedUser != null)
                        {
                            userExists = savedUser.IsUserExists;
                        }
                    }

                    if (userExists)
                    {
                        return Redirect(registerInvestorViewModel.Content.ConfirmationPage.Url);
                    }
                    else
                    {
                        //Generate resend email pref link 

                        var resendEmailPageUrl = $"/api/{Sitecore.Mvc.Configuration.MvcSettings.SitecoreRouteName}/{ControllerContext.RouteData.Values["controller"].ToString()}/ResendEmail";
                        resendEmailPageUrl = string.Format("{0}?{1}={2}&{3}=false", resendEmailPageUrl, Constants.QueryStringNames.EmailPreferencefParams.EmailQueryStringKey, registerInvestorViewModel.Email, Constants.QueryStringNames.EmailPreferencefParams.IsContactQueryStringKey);

                        var userExistsErrorMessage = registerInvestorViewModel.Content.UserExistsErrorLabel;
                        userExistsErrorMessage = userExistsErrorMessage.Replace(Constants.SitecoreTokens.RegisterUserProcess.ResendEmailLinkToken, resendEmailPageUrl);

                        registerInvestorViewModel.Error = userExistsErrorMessage;
                    }
                }
                else
                {
                    registerInvestorViewModel.Error = registerInvestorViewModel.Content.GenericErrorLabel;
                }

            }
            catch (Exception ex)
            {
                _log.Error("Exception occured when registering new user as a SF Lead.", ex, this);
                registerInvestorViewModel.Error = registerInvestorViewModel.Content.GenericErrorLabel;
            }

            return View("~/Views/MyPreferences/RegisterInvestor.cshtml", registerInvestorViewModel);
        }

        public ActionResult ResendEmail(string email, bool isContact)
        {
            var data = _context.GetDataSourceItem<IRegisterInvestor>();

            if(data == null)
            {
                return null;
            }

            var isSuccess = false;

            if (!string.IsNullOrEmpty(email))
            {
                isSuccess = _emailPreferencesService.ResendEditEmailPrefLink(email, isContact, data.ResendEditPreferencesEmailTemplate, data.EditPreferencesPage.Url);
            }
            
            if(isSuccess)
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