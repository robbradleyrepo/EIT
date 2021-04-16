using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace LionTrust.Foundation.Analytics.Goals
{
    public static class Extensions
    {
        public static HtmlString GenerateGoalAnchor<T>(this HtmlHelper<T> htmlHelper, Expression<Func<T, object>> field, Func<string> url, string cssClass, Guid goalId, Func<string> content, object parameters = null)
        {            
            return htmlHelper.Glass().Editable(field, x => $"<a href={url()} class='{cssClass}' data-goal-trigger='{goalId}'>{content()}</a>", parameters);
        }
    }
}
