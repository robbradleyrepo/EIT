using Sitecore.Publishing;
using Sitecore.Publishing.Diagnostics;
using Sitecore.Publishing.Pipelines.Publish;
namespace LionTrust.Foundation.SitecoreExtensions.Pipelines.Publish
{
    public class OverridePublishSiteContext : OverridePublishContext
    {
        public override void Process(PublishContext context)
        {
            if (IsSitePublish(context.PublishOptions) || IsRoot(context.PublishOptions))
            {
                context.PublishOptions.PublishRelatedItems = false;
                PublishingLog.Info(string.Format("SitePublish detected. PublishContext was overridden with PublishRelatedItems=false."));
            }
            else if (IsHomepage(context.PublishOptions))
            {
                context.PublishOptions.PublishRelatedItems = false;
                PublishingLog.Info(string.Format("HomepagePublish detected. PublishContext was overridden with PublishRelatedItems=false."));
            }
            return;
        }
        private bool IsRoot(PublishOptions publishOptions)
        {
            return publishOptions.RootItem == null || publishOptions.RootItem.ID == Sitecore.ItemIDs.RootID || publishOptions.RootItem.ID == Sitecore.ItemIDs.ContentRoot;
        }
        private bool IsHomepage(PublishOptions publishOptions)
        {
            return publishOptions.RootItem != null && publishOptions.RootItem.DescendsFrom(Constants.Site.Template_ID);
        }
    }
}