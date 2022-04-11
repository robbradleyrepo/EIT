namespace LionTrust.Feature.MyPreferences.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Feature.MyPreferences.Repositories;
    using LionTrust.Feature.MyPreferences.Services;
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Foundation.Contact.Services;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using LionTrust.Foundation.Contact.Models;
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Onboarding.Models;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Services.Interfaces;

    public class FundAccordionListController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly EmailPreferencesService _emailPreferencesService;
        private readonly IPersonalizedContentService _personalizedContentService;
        private readonly IFundContentSearchService _fundContentSearchService;

        public FundAccordionListController(IMvcContext context, BaseLog log, IMailManager mailManager, IEmailPreferencesRepository editEmailPreferencesRepository, IPersonalizedContentService personalizedContentService, IFundContentSearchService fundContentSearchService)
        {
            _context = context;
            _personalizedContentService = personalizedContentService;
            _emailPreferencesService = new EmailPreferencesService(editEmailPreferencesRepository, mailManager, personalizedContentService);
            _fundContentSearchService = fundContentSearchService;
        }

        public ActionResult Render()
        {
            var data = _context.GetDataSourceItem<IFundAccordionList>();
            var context = _personalizedContentService.GetContext();

            if (data == null)
            {
                return new EmptyResult();
            }

            data.SFProcessList = GetSPProcessList(context);

            return View("~/Views/MyPreferences/FundAccordionList.cshtml", data);
        }

        private IList<SFProcess> GetSPProcessList(Context context)
        {
            ContentSearchResults<FundSearchResultItem> fundSearchResults = _fundContentSearchService.GetAllAllowedFunds();
            var allowedFunds = fundSearchResults?.SearchResults?.Select(x => x.Document.SalesforceFundId)?.Where(x => !string.IsNullOrEmpty(x))?.ToList();

            var SPProcessList = _emailPreferencesService.GetSFProcessList()?.ToList();

            if (SPProcessList != null && SPProcessList.Any())
            {
                foreach (var sfProcess in SPProcessList)
                {
                    if (sfProcess.SFFundList != null && sfProcess.SFFundList.Any())
                    {
                        var allowedFundList = sfProcess.SFFundList.Where(s => allowedFunds.Contains(s.SFFundId.Substring(0, 15)));
                        sfProcess.SFFundList = allowedFundList?.ToList();
                    }
                }

                SPProcessList = SPProcessList.Where(l => l.SFFundList != null && l.SFFundList.Any())?.ToList();
            }

            if (context == null || context.Preferences == null || context.Preferences.SFProcessList == null)
            {
                return SPProcessList;
            }
            foreach (var process in context.Preferences.SFProcessList)
            {
                var currentProcess = SPProcessList.FirstOrDefault(x => x.SFProcessId == process.SFProcessId);

                if (currentProcess == null)
                {
                    continue;
                }

                currentProcess.IsProcessSelected = process.IsProcessSelected;

                foreach (var fund in process.SFFundList)
                {
                    var currentFund = currentProcess.SFFundList.FirstOrDefault(x => x.SFFundId == fund.SFFundId);

                    if (currentFund == null)
                    {
                        continue;
                    }

                    currentFund.IsFundSelected = fund.IsFundSelected;
                }
            }

            return SPProcessList;
        }
    }
}