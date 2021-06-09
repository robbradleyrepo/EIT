namespace LionTrust.Feature.Fund.FundClass
{
    using LionTrust.Foundation.Legacy.Models;
    using System.Web;

    public static class FundClassSwitcherHelper
    {
        public static string GetCitiCode(HttpContextBase context, IFund fund)
        {
            var citiCode = context.Request.QueryString.Get("graph");
            if (!string.IsNullOrEmpty(citiCode))
            {
                return citiCode;
            }

            return fund == null ? null : fund.CitiCode;
        }
    }
}