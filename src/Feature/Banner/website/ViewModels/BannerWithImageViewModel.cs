namespace LionTrust.Feature.Banner.ViewModels
{
    using LionTrust.Foundation.ORM.Extensions;
    using LionTrust.Feature.Banner.Models;

    public class BannerWithImageViewModel
    {
        public IBannerWithImage ComponentData { get; set; }     

        public string BackgroundImageStyle { get; set; }
    }
}
