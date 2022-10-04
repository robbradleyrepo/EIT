namespace LionTrust.Foundation.SitecoreExtensions.Extensions
{
    using Glass.Mapper.Sc.Fields;
    using Sitecore.Data.Items;

    public static class LinkExtensions
    {
        public static bool IsCurrentPage(this Link link, Item currentPageItem)
        {
            if (link.Type != LinkType.Internal || currentPageItem == null)
            {
                return false;
            }

            return link.TargetId == currentPageItem.ID.Guid;
        }
    }
}