namespace LionTrust.Foundation.ORM.Extensions
{
    using Glass.Mapper.Sc.Fields;

    public static class ImageExtensions
    {
        public static string GetSafeSitecoreImageUrl(this Image image, string defaultValue = "")
        {
            if (image != null && !string.IsNullOrEmpty(image.Src))
            {
                return image.Src;
            }
            else
            {
                return defaultValue;
            }
        }

        public static string GetSafeSitecoreImageAltText(this Image image, string defaultValue = "")
        {
            if (image != null && !string.IsNullOrEmpty(image.Src))
            {
                return image.Alt;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}