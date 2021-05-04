namespace LionTrust.Feature.Fund.Models
{
    using System.Linq;

    public class FundManagerViewModel
    {
        public FundManagerViewModel(IFundManagerPage managerPage)
        {
            ImageUrl = managerPage.Manager?.Image?.Src;
            FullName = managerPage.Manager?.FullName;
            Url = managerPage.Url;
            Bio = managerPage.Manager?.ShortBio;
            Title = managerPage.Manager?.Title;
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