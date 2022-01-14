using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using LionTrust.Feature.EXM.ViewModels;
using Sitecore.Mvc.Controllers;
using Sitecore.EmailCampaign.Model.Web.Settings;
using System.Collections.Specialized;
using Sitecore.Modules.EmailCampaign.Core.Crypto;
using Sitecore.Modules.EmailCampaign.Core;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailHeaderController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailHeaderController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult Header()
        {
            var currentItem = _mvcContext.ContextItem;
            var preHeader = _mvcContext.GetDataSourceItem<IHeader>();

            QueryStringEncryption.GetDefaultInstance().TryDecrypt(HttpContext.Request.QueryString, out NameValueCollection result);

            var source = ExmContext.ContactIdentifier?.Source;
            var identifier = ExmContext.ContactIdentifier?.Identifier;
            //var contactId = ExmContext.Contact != null && ExmContext.Contact.Id.HasValue ? ExmContext.Contact.Id.Value.ToString() : null;
            var messageUrl = $"/?sc_itemid={currentItem.ID}&sc_lang={currentItem.Language.Name}&ex_id_s={source}&ex_id_i={identifier}&{GlobalSettings.OnlineVersionQueryStringKey}=1";
            var model = new HeaderViewModel
            {
                PreHeader = preHeader,
                PreHeaderText = preHeader?.Text?.Replace(Constants.Tokens.MessageInBrowserUrlToken, messageUrl),
            };

            return View("~/Views/EXM/Header.cshtml", model);
        }
    }
}