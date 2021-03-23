using Sitecore.Pipelines.RenderField;

namespace LionTrust.Feature.ShowTitles.Pipelines.RenderField
{
    public class ShowTitleWhenBlank
    {
        public void Process(RenderFieldArgs args)
        {
            args.RenderParameters["show-title-when-blank"] = "true";
        }
    }
}
