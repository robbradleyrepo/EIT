namespace LionTrust.Foundation.Analytics.Goals
{
    using Glass.Mapper.Sc.Web.Mvc;
    using System;
    using System.Web;
    using System.Linq.Expressions;
    using Glass.Mapper.Sc;
    using System.Collections.Specialized;

    public static class Extensions
    {
        public static HtmlString GenerateGoalAnchor<TK, T>(this GlassHtmlMvc<TK> glass, T item, Expression<Func<T, object>> field, Guid goalId, NameValueCollection attributes = null)
        {
            if (attributes == null)
            {
                attributes = new NameValueCollection { { "data-goal-trigger", goalId.ToString("B").ToUpperInvariant() } };
            }
            else
            {
                attributes.Add("data-goal-trigger", goalId.ToString("B").ToUpperInvariant());
            }

            return glass.Editable<T>(item, field, attributes);
        }

        public static RenderingResult BeginRenderLinkWithGoal<TK, T>(this GlassHtmlMvc<TK> glass, T model, Expression<Func<T, object>> field, Guid goalId, NameValueCollection attributes = null, bool isEditable = false, bool alwaysRender = false)
        {
            if (attributes == null)
            {
                attributes = new NameValueCollection { { "data-goal-trigger", goalId.ToString("B").ToUpperInvariant() } };
            }
            else
            {
                attributes.Add("data-goal-trigger", goalId.ToString("B").ToUpperInvariant());
            }

            return glass.BeginRenderLink<T>(model, field, attributes, isEditable, alwaysRender);
        }
    }
}