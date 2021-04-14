namespace LionTrust.Foundation.LocalDatasource
{
    using Sitecore.Data;

    public class Templates
    {
        public static class RenderingOptions
        {
            public static readonly ID ID = new ID("{D1592226-3898-4CE2-B190-090FD5F84A4C}");
            public static class Fields
            {
                public static readonly ID SupportsLocalDatasource = new ID("{04437558-AC18-42CB-AB21-1243A66ABE67}");
            }
        }

        public static class Index
        {
            public static class Fields
            {
                public static readonly string LocalDatasourceContent_IndexFieldName = "local_datasource_content";
            }
        }
    }
}