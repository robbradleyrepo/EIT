using Sitecore.Mvc.Pipelines.Response.RenderRendering;

namespace LionTrust.Foundation.SitecoreExtensions.Pipelines.RenderRendering
{
    public class ClearInvalidDatasource : RenderRenderingProcessor
    {
        public override void Process(RenderRenderingArgs args)
        {
            var rendering = args.Rendering;

            if (!string.IsNullOrWhiteSpace(rendering.DataSource) && Sitecore.Context.Database.Items.GetItem(rendering.DataSource) == null)
            {
                rendering.DataSource = string.Empty;
            }
        }
    }
}