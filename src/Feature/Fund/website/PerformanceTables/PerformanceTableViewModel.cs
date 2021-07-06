namespace LionTrust.Feature.Fund.PerformanceTables
{
    using System;
    using System.Collections.Generic;

    public class PerformanceTableViewModel
    {
        public IPerformanceTable Component { get; set; }

        public PerformanceTableRow[] Rows { get; set; }

        public PerformanceTableRow QuartileRow { get; set; }

        public string[] ColumnHeadings
        {
            get
            {
                if (Component == null)
                {
                    return new string[0];
                }

                var result = new List<string>();
                for (int i = -1; i >= -4; i--)
                {
                    result.Add($"{Component.ColumnMonth} {DateTime.Now.AddYears(i).ToString("yy")}");
                }

                return result.ToArray();
            }
        }

        public PerformanceTableViewModel()
        {
            Rows = new PerformanceTableRow[0];
        }
    }
}