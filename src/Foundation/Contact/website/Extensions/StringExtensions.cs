namespace LionTrust.Foundation.Contact.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class StringExtensions
    {
        private static char[] _characterDelimeters = new[] { ',', '|', ';' };

        public static List<Guid> GetGuids(this string value, char delimeter)
        {
            var list = new List<Guid>();
            if (string.IsNullOrEmpty(value))
            {
                return list;
            }

            foreach (var guidStr in value.Split(delimeter))
            {
                Guid guid;
                if (Guid.TryParse(guidStr, out guid))
                {
                    list.Add(guid);
                }
            }
            return list;
        }

        public static string[] StandardStringSplit(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return value.Split(_characterDelimeters, StringSplitOptions.RemoveEmptyEntries);
        }

        public static Guid ToGuid(this string value)
        {
            Guid guid;
            if (Guid.TryParse(value, out guid))
            {
                return guid;
            }
            else
            {
                return new Guid();
            }
        }

        public static decimal? GetDecimal(this string value, string c = "%")
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace(c, string.Empty);
            }
            decimal tmpvalue;
            decimal? result = decimal.TryParse(value, out tmpvalue) ?
                              tmpvalue : (decimal?)null;

            return result;
        }        
    }
}
