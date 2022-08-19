namespace LionTrust.Feature.Banner.Models
{
    public class MyFundsLeadBannerViewModel
    {
        public MyFundsLeadBannerViewModel(IMyFundsLeadBanner myFundsLeadBanner)
        {
            Content = myFundsLeadBanner;
        }

        public IMyFundsLeadBanner Content { get; private set; }

        public string ContactName { get; set; }

        public bool HasQueryString => !string.IsNullOrEmpty(Content?.Cta?.Query);
    }
}