 namespace LionTrust.Foundation.Analytics.Goals
{
    using Glass.Mapper.Sc.Web.Mvc;
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq.Expressions;
    using Glass.Mapper.Sc;
    using System.Collections.Specialized;
    using Glass.Mapper.Sc.Fields;

    public class AnalyticsGlassHtmlMvc<T>
    {
        private GlassHtmlMvc<T> glass;

        public AnalyticsGlassHtmlMvc(GlassHtmlMvc<T> glass)
        {
            this.glass = glass;
        }
        public HtmlString GenerateGoalAnchor<TK>(TK item, Expression<Func<TK, object>> field, Func<string> url, string cssClass, Guid goalId, Func<string> content, object parameters = null)
        {
            return glass.Editable<TK>(item, field, x => $"<a href={url()} class='{cssClass}' data-goal-trigger='{goalId}'>{content()}</a>", parameters);
        }        
    }

    public static class Extensions
    {
        public static AnalyticsGlassHtmlMvc<T> AnalyticsGlass<T>(this GlassHtmlMvc<T> glass)
        {
            return new AnalyticsGlassHtmlMvc<T>(glass);
        }

        public static HtmlString GenerateGoalAnchor<T>(this HtmlHelper<T> htmlHelper, T item, Expression<Func<T, object>> field, Func<string> url, string cssClass, Guid goalId, Func<string> content, object parameters = null)
        {
            return htmlHelper.Glass().Editable<T>(item, field, x => $"<a href={url()} class='{cssClass}' data-goal-trigger='{goalId}'>{content()}</a>", parameters);
        }

        public static HtmlString GenerateGoalAnchor<T>(this HtmlHelper<T> htmlHelper, Expression<Func<T, object>> field, Func<string> url, string cssClass, Guid goalId, Func<string> content, object parameters = null)
        {
            return htmlHelper.Glass().Editable(field, x => $"<a href={url()} class='{cssClass}' data-goal-trigger='{goalId}'>{content()}</a>", parameters);
        }

        public static RenderingResult BeginRenderLinkWithGoal<T>(this HtmlHelper<T> htmlHelper, T model, Expression<Func<T, object>> field, Guid goalId, NameValueCollection attributes = null, bool isEditable = false, bool alwaysRender = false)
        {
            if (attributes == null)
            {
                attributes = new NameValueCollection { { "data-goal-trigger", goalId.ToString() } };
            }
            else
            {
                attributes.Add("data-goal-trigger", goalId.ToString());
            }

            return htmlHelper.Glass().BeginRenderLink<T>(model, field, attributes, isEditable, alwaysRender);
        }
    }
}
