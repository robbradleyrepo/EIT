namespace LionTrust.Feature.Fund.FundClass
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System;

    public class FundClassData
    {
        public string Comparator1 { get; set; }

        public string Comparator2 { get; set; }

        public string SinglePrice { get; set; }

        public string OfferPrice { get; set; }

        public DateTime PriceDate { get; set; }

        public DateTime ClassLaunchDate { get; set; }

        public string SectorName { get; set; }

        public DateTime ManagerInceptionDateOfFund { get; set; }

        public string TargetBenchmarkYield { get; set; }

        [SitecoreId]
        public Guid Id { get; set; }

        public string ClassLaunchDateFormatted
        {
            get
            {
                if (ClassLaunchDate != null && ClassLaunchDate != DateTime.MinValue)
                {
                    return ClassLaunchDate.ToString("dd/MM/yyyy");
                }

                return string.Empty;
            }
        }

        public string ManagerInceptionDateFormatted
        {
            get
            {
                if (ManagerInceptionDateOfFund != null && ManagerInceptionDateOfFund != DateTime.MinValue)
                {
                    return ManagerInceptionDateOfFund.ToString("dd/MM/yyyy");
                }

                return string.Empty;
            }
        }

        public string PriceDateFormatted
        {
            get
            {
                if (PriceDate != null && PriceDate != DateTime.MinValue)
                {
                    return PriceDate.ToString("dd/MM/yyyy");
                }

                return string.Empty;
            }
        }
    }
}