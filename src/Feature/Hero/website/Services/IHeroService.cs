using LionTrust.Feature.Hero.Models;
using LionTrust.Foundation.Search.Models;

namespace LionTrust.Feature.Hero.Services
{
    public interface IHeroService
    {
        IHero GetHeroItems();
        BaseSearchResultItem GetHeroImagesSearch();
        bool IsExperienceEditor { get; }
    }
}
