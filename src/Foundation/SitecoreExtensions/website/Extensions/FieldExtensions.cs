namespace LionTrust.Foundation.SitecoreExtensions.Extensions
{
    using System;

    using Sitecore;
    using Sitecore.Data.Fields;

    public static class FieldExtensions
    {
        public static bool IsChecked(this Field checkboxField)
        {
            if (checkboxField == null)
            {
                throw new ArgumentNullException(nameof(checkboxField));
            }

            return MainUtil.GetBool(checkboxField.Value, false);
        }
    }
}