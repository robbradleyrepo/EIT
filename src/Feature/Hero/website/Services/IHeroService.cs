using LionTrust.Feature.Hero.Models;
using LionTrust.Foundation.Search.Models.ContentSearch;

namespace LionTrust.Feature.Hero.Services
{
    public interface IHeroService
    {
        IHero GetHeroItems();
        BaseSearchResultItem GetHeroImagesSearch();
        bool IsExperienceEditor { get; }
    }
}
