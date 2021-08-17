namespace LionTrust.Feature.MyPreferences.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Feature.MyPreferences.Repositories;
    using LionTrust.Feature.MyPreferences.Services;
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Foundation.Contact.Models;
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

        public EditEmailPreferencesController(IMvcContext context, BaseLog log, IMailManager mailManager, IEmailPreferencesRepository editEmailPreferencesRepository)
        {
            _context = context;
            _log = log;
            _emailPreferencesService = new EmailPreferencesService(editEmailPreferencesRepository, mailManager);
        }

        public ActionResult Render(string @ref)
        {
            var data = _context.GetDataSourceItem<IEditEmailPreferences>();
            var emailPreferences = _emailPreferencesService.GetEmailPreferences(@ref);

            if (data == null)
            {
                return null;
            }

            var viewModel = new EditEmailPreferencesViewModel(emailPreferences, data);

            if (emailPreferences == null)
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
                    if (!string.IsNullOrEmpty(registerInvestorViewModel.SFEntityId) && (registerInvestorViewModel.SFEntityId.StartsWith("003", StringComparison.CurrentCultureIgnoreCase) || registerInvestorViewModel.SFEntityId.StartsWith("00Q", StringComparison.CurrentCultureIgnoreCase)) && !string.IsNullOrEmpty(registerInvestorViewModel.SFRandomGUID))
                    {
                        var emailPreferences = new EmailPreferences();
                        emailPreferences.SFEntityId = registerInvestorViewModel.SFEntityId;
                        emailPreferences.IsContact = (registerInvestorViewModel.SFEntityId.StartsWith("003", StringComparison.CurrentCultureIgnoreCase)) ? true : false;
                        emailPreferences.SFRandomGUID = registerInvestorViewModel.SFRandomGUID;
                        emailPreferences.EmailAddress = registerInvestorViewModel.EmailAddress;

                        var IsUnsubscribeAll = registerInvestorViewModel.UnsubscribeAll;
                        emailPreferences.UnsubscribeAll = IsUnsubscribeAll;
                        //If Unsubscribe All checkbox is ticked, uncheck all the Marketing checkboxes in SF                    
                        emailPreferences.IncludeInLTNews = (IsUnsubscribeAll) ? false : registerInvestorViewModel.IncludeInLTNews;
                        emailPreferences.IsInstitutionalBulletinChecked = (IsUnsubscribeAll) ? false : registerInvestorViewModel.IsInstitutionalBulletin;
                        emailPreferences.IsConsentGivenDateEmpty = registerInvestorViewModel.IsConsentGivenDateEmpty;
                        emailPreferences.IsUkResident = registerInvestorViewModel.IsUkResident;

                        var sfProcessList = new List<SFProcess>();

                        if (emailPreferences.IsUkResident)
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

                        emailPreferences.SFProcessList = sfProcessList;
                        submitSuccess = _emailPreferencesService.SaveEmailPreferences(emailPreferences);
                    }
                    else
                    {
                        _log.Debug("No correct format Salesforce Entity Id or RandomGuid found.", this);
                        submitSuccess = false;
                    }
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