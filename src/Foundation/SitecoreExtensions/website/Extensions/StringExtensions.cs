namespace LionTrust.Foundation.SitecoreExtensions.Extensions
{
    public static class StringExtensions
    {
        public static string Ellipsis(this string text, int maxLength)
        {
            if (text.Length <= maxLength || maxLength <= 0)
            {
                return text;
            }

            return text.Substring(0, maxLength - 1).Trim() + "...";
        }
    }
}