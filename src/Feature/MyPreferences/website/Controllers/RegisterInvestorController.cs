namespace LionTrust.Feature.MyPreferences.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Feature.MyPreferences.Repositories;
    using LionTrust.Feature.MyPreferences.Services;
    using LionTrust.Foundation.Analytics.Goals;
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Foundation.Contact.Models;
    using LionTrust.Foundation.Contact.Services;
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Foundation.Contact;
    using Sitecore.XConnect.Client;
    using Sitecore.XConnect.Collection.Model;
    using static LionTrust.Feature.MyPreferences.Constants;
    using QueryStringNames = Foundation.Contact.Constants.QueryStringNames;

    public class RegisterInvestorController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly BaseLog _log;
        private readonly IPersonalizedContentService _personalizedContentService;
        private readonly EmailPreferencesService _emailPreferencesService;

        public RegisterInvestorController(IMvcContext context, BaseLog log, IMailManager mailManager, IEmailPreferencesRepository editEmailPreferencesRepository, IPersonalizedContentService personalizedContentService)
        {
            _context = context;
            _log = log;
            _personalizedContentService = personalizedContentService;
            _emailPreferencesService = new EmailPreferencesService(editEmailPreferencesRepository, mailManager, personalizedContentService);
        }

        public ActionResult RegisterInvestor(Errors error = Errors.None, string email = "")
        {
            var data = _context.GetDataSourceItem<IRegisterInvestor>();
            var home = _context.GetHomeItem<IHome>();

            if (data == null || home == null)
            {
                return null;
            }

            var investor = OnboardingHelper.GetCurrentContactInvestor(_context, _log);


            if (!Sitecore.Context.PageMode.IsExperienceEditor && investor == null)
            {
                return null;
            }

            var viewModel = new RegisterInvestorViewModel(data);

            viewModel.CountryName = OnboardingHelper.GetCurrentContactCountry(_context)?.CountryName;
            viewModel.ChangeInvestorUrl = OnboardingHelper.GetChangeUrl();

            if (!Sitecore.Context.PageMode.IsExperienceEditor)
            {
                viewModel.ProfessionalInvestor = investor.Id != home.OnboardingConfiguration.PrivateInvestor?.Id;
                viewModel.InvestorType = investor.InvestorName;
            }

            //Workaround so model state works as company name & FSA required if professional
            if (!viewModel.ProfessionalInvestor)
            {
                viewModel.CompanyName = !string.IsNullOrEmpty(data.CompanyFieldDefaultValue) ? data.CompanyFieldDefaultValue : "Self";
                viewModel.CompanyId = "000000";
            }

            if (error == Errors.UserExists)
            {
                viewModel.Error = data.UserExistsErrorLabel;
                viewModel.UserExists = true;
            }
            else if (error == Errors.General)
            {
                viewModel.Error = data.GenericErrorLabel;
            }
            else if (error == Errors.EmailNotRecognized)
            {
                viewModel.Error = data.RetrievePreferencesEmailNotRecognized;
                viewModel.EmailNotRecognized = true;
            }

            return View("~/Views/MyPreferences/RegisterInvestor.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult RegisterInvestor(RegisterInvestorSubmit registerInvestorSubmit)
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
                    var ukResident = OnboardingHelper.IsUkResident();
                    var emailTemplate = ukResident ? data.UKEmailTemplate : data.NonUKEmailTemplate;

                    if (!registerInvestorSubmit.ProfessionalInvestor)
                    {
                        var nonProfUserViewModel = new NonProfessionalUser
                        {
                            FirstName = registerInvestorSubmit.FirstName,
                            LastName = registerInvestorSubmit.LastName,
                            Email = registerInvestorSubmit.Email,
                            IsUKResident = ukResident,
                            Company = !string.IsNullOrEmpty(data.CompanyFieldDefaultValue) ? data.CompanyFieldDefaultValue : "Self",
                            Unsubscribed = !registerInvestorSubmit.SubscribeToEmail
                    };

                        var savedUser = _emailPreferencesService.SaveNonProfUserAsSFLead(nonProfUserViewModel, emailTemplate, data.EditPreferencesPage.AbsoluteUrl, data.FundDashboardPage.AbsoluteUrl);

                        if (savedUser != null)
                        {
                            userExists = savedUser.IsUserExists;
                        }
                    }
                    else
                    {
                        var sfOrganisationId = (!string.IsNullOrEmpty(data.DefaultSFOrganisationId)) ? data.DefaultSFOrganisationId : string.Empty;
                        var professionalUser = new ProfessionalUser
                        {
                            FirstName = registerInvestorSubmit.FirstName,
                            LastName = registerInvestorSubmit.LastName,
                            Email = registerInvestorSubmit.Email,
                            IsUKResident = ukResident,
                            CompanyId = registerInvestorSubmit.CompanyId,
                            Company = registerInvestorSubmit.CompanyName,
                            Organisation = sfOrganisationId,
                            Unsubscribed = !registerInvestorSubmit.SubscribeToEmail
                        };

                        var savedUser = _emailPreferencesService.SaveProfUserAsSFContact(professionalUser, emailTemplate, data.EditPreferencesPage.AbsoluteUrl, data.FundDashboardPage.AbsoluteUrl);

                        if (savedUser != null)
                        {
                            userExists = savedUser.IsUserExists;
                        }
                    }

                    if (!userExists)
                    {
                        var context = _personalizedContentService.GetContext();
                        if (UpdateEmailPreferences(registerInvestorSubmit, context))
                        {
                            var country = OnboardingHelper.GetCurrentContactCountry(_context);
                            
                            using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                            {
                                OnboardingHelper.UpdateContactSession(null);
                                var contact = OnboardingHelper.GetContact(client);
                                
                                if (contact != null)
                                {
                                    var address = new Address
                                    {
                                        CountryCode = country.ISO
                                    };

                                    if (contact.Addresses() != null)
                                    {
                                        contact.Addresses().PreferredAddress = address;
                                    }
                                    else
                                    {
                                        client.SetAddresses(contact, new AddressList(address, AddressList.DefaultFacetKey));
                                    }

                                    client.Submit();
                                    OnboardingHelper.UpdateContactSession(contact);
                                }
                                
                            }
                            
                            return Redirect(data.ConfirmationPage.Url);
                        }
                        else
                        {
                            error = Errors.General;
                        }
                    }
                    else
                    {
                        error = Errors.UserExists;
                        return Redirect($"{Request.Url.GetLeftPart(UriPartial.Path)}?{QueryStringNames.EmailPreferencefParams.ErrorQueryStringKey}={(int)error}&{QueryStringNames.EmailPreferencefParams.EmailQueryStringKey}={registerInvestorSubmit.Email}#retrieve-preferences");
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

            return Redirect($"{Request.Url.GetLeftPart(UriPartial.Path)}?{QueryStringNames.EmailPreferencefParams.ErrorQueryStringKey}={(int)error}");
        }

        public ActionResult ResendEmail(string email, Guid dataSourceId, bool isContact)
        {
            var error = Errors.None;
            var data = _context.SitecoreService.GetItem<IRegisterInvestor>(dataSourceId);

            if (data == null)
            {
                return null;
            }

            var isSuccess = false;

            if (!string.IsNullOrEmpty(email))
            {
                isSuccess = _emailPreferencesService.ResendEditEmailPrefLink(email, isContact, data.ResendEditPreferencesEmailTemplate, data.EditPreferencesPage.AbsoluteUrl, data.FundDashboardPage.AbsoluteUrl);
            }

            if (isSuccess)
            {
                // trigger goal
                var goal = data.RetrievePreferencesGoal;
                if (goal != Guid.Empty)
                {
                    Helper.TriggerGoal(new Sitecore.Data.ID(goal));
                }

                return Redirect(data.ResendEmailSuccessPage.Url);
            }
            else
            {
                error = Errors.EmailNotRecognized;                
                return Redirect($"{data.ResendEmailFailedPage.Url}?{QueryStringNames.EmailPreferencefParams.ErrorQueryStringKey}={(int)error}&{QueryStringNames.EmailPreferencefParams.EmailQueryStringKey}={email}#retrieve-preferences");
            }
        }

        private bool UpdateEmailPreferences(RegisterInvestorSubmit registerInvestorSubmit, Context context)
        {
            context.Preferences.EmailAddress = registerInvestorSubmit.Email;

            if(registerInvestorSubmit.SubscribeToEmail)
            {
                context.Preferences.SubscribeToInsights();
            }
            else
            {
                context.Preferences.UnsubscribeAll();
            }

            context.Preferences.IsUkResident = OnboardingHelper.IsUkResident();

            var sfProcessList = new List<SFProcess>();

            //Iterate through process repeater
            foreach (var fundCategory in registerInvestorSubmit.SFProcessList)
            {
                //Generate SFProcessViewModel
                var sfProcess = new SFProcess();
                sfProcess.SFProcessId = fundCategory.SFProcessId;
                sfProcess.IsProcessSelected = fundCategory.IsProcessSelected;

                //Iterate through fund repeater
                var sfFundList = new List<SFFund>();

                foreach (var fund in fundCategory.SFFundList)
                {
                    var sfFund = new SFFund();
                    sfFund.SFFundId = fund.SFFundId;
                    sfFund.IsFundSelected = fund.IsFundSelected;
                    sfFundList.Add(sfFund);

                }

                sfProcess.SFFundList = sfFundList;
                sfProcessList.Add(sfProcess);
            }

            context.Preferences.SFProcessList = sfProcessList;

            return _emailPreferencesService.SaveEmailPreferences(context);

        }
    }
}