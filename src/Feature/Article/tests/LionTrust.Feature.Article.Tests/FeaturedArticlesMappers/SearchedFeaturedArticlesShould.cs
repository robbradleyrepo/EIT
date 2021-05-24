namespace LionTrust.Feature.Article.Tests.RelatedArticlesMappers
{
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Article.FeaturedArticleMappers;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Moq;
    using NUnit.Framework;
    using Sitecore.Abstractions;

    [TestFixture]
    public class SearchedRelatedArticlesShould
    {
        private Mock<IArticleContentSearchService> searchService;
        private Mock<BaseLinkManager> linkManager;

        [SetUp]
        public void Setup()
        {
            searchService = new Mock<IArticleContentSearchService>();
            linkManager = new Mock<BaseLinkManager>();
        }

        [Test]
        public void ReturnAnEmptyListIfInputIsNull()
        {
            var target = new SearchedRelatedArticles(searchService.Object, linkManager.Object);
            var filter = new Mock<IArticleFilter>();
            var result = target.Map(filter.Object, "dave");
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }      
    }
}
