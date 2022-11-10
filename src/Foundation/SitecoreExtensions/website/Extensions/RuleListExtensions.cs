using Sitecore.Rules;
using System;

namespace LionTrust.Foundation.SitecoreExtensions.Extensions
{
    public static class RuleListExtensions
    {
        //NOTE: copied from https://www.coreysmith.co/glass-mapper-map-sitecore-rules-fields/
        public static bool EvaluateRules<T>(this RuleList<T> ruleList, T ruleContext)
              where T : RuleContext
        {
            if (ruleList == null) throw new ArgumentNullException(nameof(ruleList));
            if (ruleContext == null) throw new ArgumentNullException(nameof(ruleContext));
            if (ruleContext.IsAborted || ruleList.Count == 0) return false;

            foreach (var rule in ruleList.Rules)
            {
                if (rule.Condition == null) continue;
                var result = rule.Evaluate(ruleContext);
                if (ruleContext.IsAborted) return false;
                if (result) return true;
            }
            return false;
        }

        public static bool EvaluateRules(this RuleList<RuleContext> ruleList, bool emptyResult = true)
        {
            if (ruleList == null || ruleList.Count == 0) return emptyResult;

            var ruleContext = new RuleContext()
            {
                Item = Sitecore.Context.Item
            };

            return ruleList.EvaluateRules(ruleContext);
        }
    }
}