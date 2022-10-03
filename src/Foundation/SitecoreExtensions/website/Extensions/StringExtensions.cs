namespace LionTrust.Foundation.SitecoreExtensions.Extensions
{
    public static class StringExtensions
    {
        public static string Ellipsis(this string text, int length)
        {
            if (text.Length <= length)
            {
                return text;
            }

            return text.Substring(0, length - 1).Trim() + "...";
        }
    }
}