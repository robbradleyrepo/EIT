namespace LionTrust.Feature.Promo
{
    using Glass.Mapper.Sc.Fields;

    public static class Extensions
    {
        public static string GetSafeSitecoreImageUrl(Image image)
        {
            return image == null ? string.Empty : image.Src;
        }

        public static string GetSafeSitecoreImageAltText(Image image)
        {
            return image == null ? string.Empty : image.Alt;
        }
    }
}