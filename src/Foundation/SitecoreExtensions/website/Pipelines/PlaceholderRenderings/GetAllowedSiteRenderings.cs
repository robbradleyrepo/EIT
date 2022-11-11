using LionTrust.Foundation.SitecoreExtensions.Placeholders;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetPlaceholderRenderings;
using System.Collections.Generic;

namespace LionTrust.Foundation.SitecoreExtensions.Pipelines.PlaceholderRenderings
{
    public class GetAllowedSiteRenderings : GetAllowedRenderings
    {
        public new void Process(GetPlaceholderRenderingsArgs args)
        {
            Assert.IsNotNull(args, nameof(args));

            Item placeholderItem = null;
            var pageContext = new CustomPageContext();

            if (ID.IsNullOrEmpty(args.DeviceId))
            {
                placeholderItem = pageContext.GetPlaceholderItem(args.PlaceholderKey, args.ContentDatabase, args.LayoutDefinition);
            }
            else
            {
                using (new DeviceSwitcher(args.DeviceId, args.ContentDatabase))
                {
                    placeholderItem = pageContext.GetPlaceholderItem(args.PlaceholderKey, args.ContentDatabase, args.LayoutDefinition);
                }
            }

            List<Item> objList = null;
            if (placeholderItem != null)
            {
                args.HasPlaceholderSettings = true;
                objList = GetRenderings(placeholderItem, out bool allowedControlsSpecified);
                if (allowedControlsSpecified)
                {
                    args.Options.ShowTree = false;
                }
            }

            if (objList == null)
            {
                return;
            }

            if (args.PlaceholderRenderings == null)
            {
                args.PlaceholderRenderings = new List<Item>();
            }

            args.PlaceholderRenderings.AddRange(objList);
        }
    }
}