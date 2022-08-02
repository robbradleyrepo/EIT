namespace LionTrust.Feature.Fund.FundStats
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.FundClass;
    using LionTrust.Feature.Fund.Models;
    using LionTrust.Feature.Fund.Repository;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Linq;
    using System.Text;
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

            var viewModel = new FundStatsViewModel()
            {
                FundSelector = datasource,
                ManagedLength = datasource.ManagedByCurrentTeamForValue
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

                if (string.IsNullOrEmpty(datasource.ManagedByCurrentTeamForValue))
                {
                    // compute the Manager Inception Date as the oldest out of all the fund classes
                    var managerInceptionDate = fundClass.ManagerInceptionDateOfFund != DateTime.MinValue ? fundClass.ManagerInceptionDateOfFund : DateTime.MaxValue;
                    foreach (var cls in fund.Classes.Where(c => c.CitiCode != citiCode))
                    {
                        if (cls.ManagerInceptionDateOfFund != DateTime.MinValue && cls.ManagerInceptionDateOfFund < managerInceptionDate)
                        {
                            managerInceptionDate = cls.ManagerInceptionDateOfFund;
                        }
                    }

                    viewModel.ManagedLength = GetManagedLength(managerInceptionDate != DateTime.MaxValue ? managerInceptionDate : datasource.Fund.LaunchDate);
                }
            }

            return View("/views/fund/FourFundStats.cshtml", viewModel);
        }

        public ActionResult RenderOnDemand()
        {
            var datasource = _context.GetDataSourceItem<IFourFundStatsOnDemand>();
            if (datasource == null)
            {
                return null;
            }
          
            var pageData = _context.GetPageContextItem<IPresentationBase>();
            var fund = pageData?.Fund;

            if (fund == null)
            {
                return new EmptyResult();
            }

            var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, fund);

            var viewModel = new FundStatsOnDemandViewModel()
            {
                FundSelector = datasource
            };

            var fundClass = fund.Classes.Where(c => c.CitiCode == citiCode).FirstOrDefault();
            if (fundClass != null)
            {
                var fundValues = _fundRepository.GetFundStatsDetailsOnDemand(fundClass, "0");
                if (fundValues != null)
                {
                    fundValues.SharePrice = GetSharePriceFormatted(fundValues.SharePrice);
                    viewModel.FundValues = fundValues;
                }                
            }

            return View("/views/fund/FourFundStatsOnDemand.cshtml", viewModel);
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

        private string GetSharePriceFormatted(string sharePrice)
        {
            if (string.IsNullOrWhiteSpace(sharePrice))
            {
                return sharePrice;
            }

            //Eg. 748.844p - This regex gets the decimal value from the string.
            var decimalsFromString = Regex.Match(sharePrice, @"(\d+(\.\d+)?)|(\.\d+)")?.Value;
            var twoDecimalsString = FundRepository.GetTwoDecimalsFormat(decimalsFromString);
            sharePrice = $"{twoDecimalsString} GBX";
           
            return sharePrice;
        }        

        private string GetManagedLength(DateTime launchDate)
        {
            if (launchDate == null)
            {
                return null;
            }

            var now = DateTime.Today;
            var lengthCount = now.Year - launchDate.Year;
            var lengthCountMonths = now.Month - launchDate.Month;
            if (lengthCountMonths < 0)
            {
                lengthCount--;
                lengthCountMonths += 12;
            }

            var label = new StringBuilder();

            if(lengthCount > 0)
            {
                label.Append(Sitecore.Globalization.Translate.Text("Year"));
            }
            else
            {
                if(lengthCountMonths > 0)
                {
                    lengthCount = lengthCountMonths;
                    label.Append(Sitecore.Globalization.Translate.Text("Month"));
                }
                else
                {
                    lengthCount = now.Day - launchDate.Day;
                    label.Append(Sitecore.Globalization.Translate.Text("Day"));
                }
            }

            if (lengthCount > 1)
            {
                label = label.Append("s");
            }

            return $"{lengthCount} {label}";
        }
    }
}