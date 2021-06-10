namespace LionTrust.Foundation.SitecoreForms.Factories
{
    using LionTrust.Foundation.SitecoreForms.Models;

    public class ViewModelFactory : IViewModelFactory
    {
        public LinkViewModel BuildLink(IProperty<Link> link)
        {
            return new LinkViewModel()
            {
                EditableLink = link.Render(),
                Url = link.Value.Url,
                Text = link.Value.Text
            };
        }
    }
}
