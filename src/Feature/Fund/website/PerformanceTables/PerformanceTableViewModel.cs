namespace LionTrust.Feature.Fund.PerformanceTables
{
    using System;
    using System.Collections.Generic;

    public class PerformanceTableViewModel
    {
        public IPerformanceTable Component { get; set; }

        public PerformanceTableRow[] Rows { get; set; }

        public PerformanceTableRow QuartileRow { get; set; }

        public virtual string[] ColumnHeadings { get; set; }

        public bool Hide { get; set; }

        public PerformanceTableViewModel()
        {
            Rows = new PerformanceTableRow[0];
        }


    }
}