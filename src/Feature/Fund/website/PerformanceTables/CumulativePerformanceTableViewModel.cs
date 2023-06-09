﻿namespace LionTrust.Feature.Fund.PerformanceTables
{
    using System;

    public class CumulativePerformanceTableViewModel: PerformanceTableViewModel
    {
        public override string[] ColumnHeadings { get; set; } = new string[]
        {
            "1 month", "3 months", "6 months", "YTD", "1 year", "3 years", "5 years", "10 years", "Since Inception"
        };
    }
}