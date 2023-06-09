﻿namespace LionTrust.Feature.Fund.Api
{
    using LionTrust.Foundation.DI;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(IFundHoldingsBreakdown), Lifetime = Lifetime.Singleton)]
    public class BasicFundHoldingsBreakdown : IFundHoldingsBreakdown
    {
        private readonly IFundClassRepository _repository;

        public BasicFundHoldingsBreakdown(IFundClassRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<FundBreakdownModel> GetFundHoldings(string citiCode)
        {
            var fundDetails = _repository.GetData();
            if (fundDetails == null)
            {
                return new FundBreakdownModel[0];
            }

            var dataForClass = fundDetails.FirstOrDefault(c => c.CitiCode == citiCode);
            if (dataForClass == null)
            {
                _repository.SendEmailOnErrorForCiticode(citiCode);
                return new FundBreakdownModel[0];
            }

            return dataForClass.Holdings.Breakdowns.Data.Select(b => new FundBreakdownModel { Name = b.Name, Weight = b.Weight });
        }
    }
}