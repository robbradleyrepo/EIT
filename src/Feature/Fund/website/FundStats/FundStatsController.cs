namespace LionTrust.Feature.Fund.FundStats
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.FundClass;
    using LionTrust.Feature.Fund.Models;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    public class FundStatsController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly IFundStatsDetails _fundRepository;
        public FundStatsController(IMvcContext context, IFundStatsDetails fundRepository)
        {
            _context = context;
            _fundRepository = fundRepository;
        }

        public ActionResult Render()
        {
            var datasource = _context.GetDataSourceItem<IFourFundStats>();
            if (datasource == null)
            {
                return null;
            }

            if (datasource.Fund == null)
            {
                return View("/views/fund/FourFundStats.cshtml", new FundStatsViewModel { FundSelector = datasource });
            }

            var pageData = _context.GetPageContextItem<IFundSelector>();
            var fund = datasource.Fund != null ? datasource.Fund : pageData?.Fund;

            if (fund == null)
            {
                return new EmptyResult();
            }

            var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, fund);



            datasource.Fund.FundSize = GetFundSizeFormatted(fund.FundSize);


            var viewModel = new FundStatsViewModel()
            {
                FundSelector = datasource
            };

            var fundClass = fund.Classes.Where(c => c.CitiCode == citiCode).FirstOrDefault();
            if (fundClass != null)
            {
                var fundValues = _fundRepository.GetFundStatsDetails(fundClass);
                if (fundValues != null)
                {
                    fundValues.FundSize = GetFundSizeFormatted(fundValues.FundSize);
                    viewModel.FundValues = fundValues;
                }
            }

            return View("/views/fund/FourFundStats.cshtml", viewModel);
        }

        private string GetFundSizeFormatted(string fundSize)
        {
            if (string.IsNullOrWhiteSpace(fundSize))
            {
                return fundSize;
            }

            decimal fundSizeDecimal;

            //Eg. £12874748.8445 - This regex gets the decimal value from the string.
            var decimalsFromString = Regex.Match(fundSize, @"(\d+(\.\d+)?)|(\.\d+)")?.Value;
            if (decimal.TryParse(decimalsFromString, out fundSizeDecimal))
            {
                //First char is normally the currency.
                var currencyChar = fundSize.ToArray().FirstOrDefault(x => char.IsSymbol(x));

                //format with currency, commas and decimal eg. £12,000.56
                fundSize = string.Format("{0}{1:n}", currencyChar, fundSizeDecimal);
            }

            return fundSize;
        }
    }
}