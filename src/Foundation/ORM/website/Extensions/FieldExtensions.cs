namespace LionTrust.Foundation.ORM.Extensions
{
    using Glass.Mapper.Sc.Fields;

    public static class FieldExtensions
    {    
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

        public static string LinkText(this Link link, string defaultValue = "")
        {
            if (link != null && !string.IsNullOrEmpty(link.Text))
            {
                return link.Text;
            }
            else
            {
                return defaultValue;
            }
        }

        public static bool NotNullOrExperienceEditor(this Link link)
        {
            return link != null || Sitecore.Context.PageMode.IsExperienceEditor;
        }
    }
}