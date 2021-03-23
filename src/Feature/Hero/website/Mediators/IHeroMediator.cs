using LionTrust.Feature.Hero.ViewModels;
using LionTrust.Foundation.Core.Models;

namespace LionTrust.Feature.Hero.Mediators
{
    public interface IHeroMediator
    {
        MediatorResponse<HeroViewModel> RequestHeroViewModel();
    }
}
