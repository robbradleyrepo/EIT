namespace LionTrust.Feature.MyPreferences.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Foundation.Contact.Models.EditEmailPreferences;
    using LionTrust.Foundation.Contact.Repositories;
    using LionTrust.Foundation.Contact.Services;
    using LionTrust.Foundation.Onboarding.Helpers;
    using Sitecore.Abstractions;
    using Sitecore.Annotations;
    using Sitecore.Data;
    using Sitecore.Links;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Web.Mvc;
    using static LionTrust.Foundation.Onboarding.Constants;

    public class RegisterInvestorController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly BaseLog _log;
        private readonly EmailPreferencesService _emailPreferencesService;

        public RegisterInvestorController(IMvcContext context, BaseLog log, ILabelsRepository labelsRepository, IMailService mailManager, IEmailPreferencesRepository editEmailPreferencesRepository)
        {
            _context = context;
            _log = log;
            _emailPreferencesService = new EmailPreferencesService(editEmailPreferencesRepository, labelsRepository, mailManager);
        }

        public ActionResult Render()
        {
            var data = _context.GetDataSourceItem<IRegisterInvestor>();

            if (data == null)
            {
                return null;
            }

            //OnboardingHelper.GetInvestorType()

            var viewModel = new RegisterInvestorViewModel(data, InvestorType.Private);

            return View("~/Views/MyPreferences/RegisterInvestor.cshtml", viewModel);
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
                    var successPageId = ID.Null;

                    if (registerInvestorViewModel.InvestorType == InvestorType.Private)
                    {
                        successPageId = new ID(Foundation.Contact.Constants.ItemIds.Content.Global.EmailPreferences.RegisterNonProfUserSuccess);

                        var company = (!string.IsNullOrEmpty(registerInvestorViewModel.Content.CompanyFieldDefaultValue)) ? registerInvestorViewModel.Content.CompanyFieldDefaultValue : "Self";

                        var nonProfUserViewModel = new NonProfessionalUser
                        {
                            FirstName = registerInvestorViewModel.FirstName,
                            LastName = registerInvestorViewModel.LastName,
                            Email = registerInvestorViewModel.Email,
                            IsUKResident = registerInvestorViewModel.UKResident,
                            Company = company
                        };

                        var resultObj = _emailPreferencesService.SaveNonProfUserAsSFLead(nonProfUserViewModel);

                        if (resultObj != null)
                        {
                            userExists = resultObj.IsUserExists;
                        }
                    }
                    else if (registerInvestorViewModel.InvestorType == InvestorType.Professional)
                    {
                        successPageId = new ID(Foundation.Contact.Constants.ItemIds.Content.Global.EmailPreferences.RegisterProfUserSuccess);
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

                        var resultObj = _emailPreferencesService.SaveProfUserAsSFContact(profUserViewModel);

                        if (resultObj != null)
                        {
                            userExists = resultObj.IsUserExists;
                        }
                    }

                    if (userExists)
                    {
                        Response.Redirect(LinkManager.GetItemUrl(Sitecore.Context.Database.GetItem(successPageId)), false);
                    }
                    else
                    {
                        //Generate resend email pref link 
                        var resendEmailPageUrl = LinkManager.GetItemUrl(Sitecore.Context.Database.GetItem(new ID(Foundation.Contact.Constants.ItemIds.Content.Global.EmailPreferences.ResendEmailPrefLinkPage)));
                        resendEmailPageUrl = string.Format("{0}?{1}={2}&{3}=false", resendEmailPageUrl, Foundation.Contact.Constants.QueryStringNames.EmailPreferencefParams.EmailQueryStringKey, registerInvestorViewModel.Email, Foundation.Contact.Constants.QueryStringNames.EmailPreferencefParams.IsContactQueryStringKey);

                        var userExistsErrorMessage = registerInvestorViewModel.Content.UserExistsErrorLabel;
                        userExistsErrorMessage = userExistsErrorMessage.Replace(Foundation.Contact.Constants.SitecoreTokens.RegisterUserProcess.ResendEmailLinkToken, resendEmailPageUrl);

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
    }
}