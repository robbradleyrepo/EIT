namespace LionTrust.Feature.Fund.FundManagerPromo
{
    using LionTrust.Foundation.Legacy.Models;
    using System.Linq;

    public class FundManagerViewModel
    {
        public FundManagerViewModel(IAuthor manager)
        {
            if (manager != null)
            {
                ImageUrl = manager.Image?.Src;
                FullName = manager.FullName;
                Url = manager.Page?.Url;
                Bio = manager.ShortBio;
                Title = manager.Title;
            }
        }

        public string ImageUrl { get; private set; }

        public string FullName { get; private set; }

        public string Url { get; private set; }

        public string Bio { get; private set; }

        public string Title { get; set; }

        public string FirstName
        {
            get
            {
                return FullName?.Split(' ').FirstOrDefault();
            }
        }
    }
}