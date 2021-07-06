using System.Linq;

namespace LionTrust.Feature.Fund.PerformanceTables
{
    public class PerformanceTableRow
    {
        public PerformanceTableRow(string name, string[] values)
        {
            this.Name = name;
            this.Columns = BuildColumns(values);
        }

        public string Name { get; set; }

        public double?[] Columns { get; set; }

        private static double?[] BuildColumns(string[] columns)
        {
            return columns.Select(c => StripPercentageAndConvertToDouble(c)).ToArray();
        }

        private static double? StripPercentageAndConvertToDouble(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            input = input.Replace("%", string.Empty);

            if (double.TryParse(input, out double result))
            {
                return result;
            }

            return null;
        }
    }
}