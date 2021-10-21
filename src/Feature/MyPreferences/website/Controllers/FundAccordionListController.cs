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

    public class FundAccordionListController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly EmailPreferencesService _emailPreferencesService;
        private readonly IPersonalizedContentService _personalizedContentService;

        public FundAccordionListController(IMvcContext context, BaseLog log, IMailManager mailManager, IEmailPreferencesRepository editEmailPreferencesRepository, IPersonalizedContentService personalizedContentService)
        {
            _context = context;
            _personalizedContentService = personalizedContentService;
            _emailPreferencesService = new EmailPreferencesService(editEmailPreferencesRepository, mailManager, personalizedContentService);
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
            var SPProcessList = _emailPreferencesService.GetSFProcessList()?.ToList();


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