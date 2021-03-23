using LionTrust.Feature.Hero.Models;
using LionTrust.Feature.Hero.ViewModels;

namespace LionTrust.Feature.Hero.Factories
{
    public interface IHeroViewModelFactory
    {
        HeroViewModel CreateHeroViewModel(IHero heroItemDataSource, bool isExperienceEditor);
    }
}
