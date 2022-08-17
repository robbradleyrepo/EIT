using Sitecore.Data.Items;
using Sitecore.Shell.Controls.RichTextEditor;
using Telerik.Web.UI;

namespace LionTrust.Foundation.SitecoreExtensions.Pipelines.RichTextEditor
{
	public class RichTextEditorCustomConfiguration : EditorConfiguration
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RichTextEditorCustomConfiguration"></see> class.
		/// </summary>
		/// <param name="profile">The profile.
		public RichTextEditorCustomConfiguration(Item profile)
			: base(profile)
		{
		}

		/// <summary>
		/// Setup editor filters.
		/// </summary>
		protected override void SetupFilters()
		{
			this.Editor.DisableFilter(EditorFilters.ConvertTags);
			this.Editor.DisableFilter(EditorFilters.MozEmStrong);
			this.Editor.EnableFilter(EditorFilters.IndentHTMLContent);
			base.SetupFilters();
		}
	}
}