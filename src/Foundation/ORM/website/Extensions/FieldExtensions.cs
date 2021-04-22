namespace LionTrust.Foundation.ORM.Extensions
{
    using Glass.Mapper.Sc.Fields;

    public  static class FieldExtensions
    {
        public static string ImageSrc(this Image image, string defaultValue = "")
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

        public static string LinkUrl(this Link link, string defaultValue = "")
        {
            if (link != null && !string.IsNullOrEmpty(link.Url))
            {
                return link.Url;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}