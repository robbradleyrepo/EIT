namespace LionTrust.Feature.Fund.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public interface IFundClassDataApi
    {
        IEnumerable<FundClassDataModel> GetFundClassData(string citiCode, string fundClassId);
    }
}