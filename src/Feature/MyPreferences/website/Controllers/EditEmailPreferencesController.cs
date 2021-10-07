namespace LionTrust.Feature.MyPreferences.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Feature.MyPreferences.Repositories;
    using LionTrust.Feature.MyPreferences.Services;
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Foundation.Contact.Models;
    using LionTrust.Foundation.Contact.Services;
    using LionTrust.Foundation.Onboarding.Helpers;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class EditEmailPreferencesController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly BaseLog _log;
        private readonly EmailPreferencesService _emailPreferencesService;
        private readonly IPersonalizedContentService _personalizedContentService;

        public EditEmailPreferencesController(IMvcContext context, BaseLog log, IMailManager mailManager, IEmailPreferencesRepository editEmailPreferencesRepository, IPersonalizedContentService personalizedContentService)
        {
            _context = context;
            _log = log;
            _personalizedContentService = personalizedContentService;
            _emailPreferencesService = new EmailPreferencesService(editEmailPreferencesRepository, mailManager, personalizedContentService);
        }

        public ActionResult Render()
        {
            var data = _context.GetDataSourceItem<IEditEmailPreferences>();
            var context = _personalizedContentService.GetContext();

            if (data == null || context == null)
            {
                return null;
            }

            var viewModel = new EditEmailPreferencesViewModel(context, data);

            if (context.Preferences == null)
            {
                viewModel.ErrorText = data.GenericError;
            }

            return View("~/Views/MyPreferences/EditEmailPreferences.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult Submit(EditEmailPreferencesViewModel registerInvestorViewModel)
        {
            var submitSuccess = true;

            try
            {
                if (ModelState.IsValid)
                {
                    var context = _personalizedContentService.GetContext();

                    context.Preferences.EmailAddress = registerInvestorViewModel.EmailAddress;

                    var IsUnsubscribeAll = registerInvestorViewModel.UnsubscribeAll;
                    context.Preferences.Unsubscribe = IsUnsubscribeAll;
                    //If Unsubscribe All checkbox is ticked, uncheck all the Marketing checkboxes in SF                    
                    context.Preferences.IncludeInLTNews = (IsUnsubscribeAll) ? false : registerInvestorViewModel.IncludeInLTNews;
                    context.Preferences.IsInstitutionalBulletinChecked = (IsUnsubscribeAll) ? false : registerInvestorViewModel.IsInstitutionalBulletin;
                    context.Preferences.IsConsentGivenDateEmpty = registerInvestorViewModel.IsConsentGivenDateEmpty;
                    context.Preferences.IsUkResident = OnboardingHelper.IsUkResident();

                    var sfProcessList = new List<SFProcess>();

                    if (context.Preferences.IsUkResident)
                    {
                        //Iterate through process repeater
                        foreach (var fundCategory in registerInvestorViewModel.SFProcessList)
                        {
                            //Generate SFProcessViewModel
                            var sfProcess = new SFProcess();
                            sfProcess.SFProcessId = fundCategory.SFProcessId;
                            sfProcess.IsProcessSelected = (IsUnsubscribeAll) ? false : fundCategory.IsProcessSelected;

                            //Iterate through fund repeater
                            var sfFundList = new List<SFFund>();

                            foreach (var fund in fundCategory.SFFundList)
                            {
                                var sfFund = new SFFund();
                                sfFund.SFFundId = fund.SFFundId;
                                sfFund.IsFundSelected = (IsUnsubscribeAll) ? false : fund.IsFundSelected;
                                sfFundList.Add(sfFund);

                            }

                            sfProcess.SFFundList = sfFundList;
                            sfProcessList.Add(sfProcess);
                        }
                    }

                    context.Preferences.SFProcessList = sfProcessList;
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

            var redirectUrl = registerInvestorViewModel.Content.SuccessPage.Url;

            if (!submitSuccess)
            {
                redirectUrl = registerInvestorViewModel.Content.FailedPage.Url;
            }

            return Redirect(redirectUrl);
        }
    }
}