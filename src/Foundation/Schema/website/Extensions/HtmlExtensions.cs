using System.Text;
using System.Web.Mvc;

namespace LionTrust.Foundation.Schema.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString RenderJson(this HtmlHelper helper, object model, bool wrapSrcipt = true, string mimeType = "application/ld+json")
        {
            if (model == null)
            {
                return new MvcHtmlString(string.Empty);
            }

            var scriptTagBegin = (!string.IsNullOrEmpty(mimeType) && !string.IsNullOrWhiteSpace(mimeType)) ? $"<script type=\"{mimeType}\">" : "<script>";
            var stringBuilder = wrapSrcipt
                ? new StringBuilder(scriptTagBegin)
                : new StringBuilder();

            stringBuilder.Append(model);

            if (wrapSrcipt)
            {
                stringBuilder.Append("</script>");
            }

            return new MvcHtmlString(stringBuilder.ToString());
        }
    }
}