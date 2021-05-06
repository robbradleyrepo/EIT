namespace LionTrust.Feature.Article.Tests.RelatedArticlesMappers
{
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Article.RelatedArticleMappers;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class UrlLinkShould
    {
        [Test]
        public void ReturnAnEmptyListIfThereAreNoChildren()
        {
            var featuredArticles = new Mock<IFeaturedArticles>();
            var result = UrlLink.Map(featuredArticles.Object);
            Assert.NotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void ReturnAMappedListIfThereAreChildren()
        {
            var featuredArticles = new Mock<IFeaturedArticles>();
            var article = new Mock<ILink>();
            article.SetupGet(a => a.Link).Returns(new Glass.Mapper.Sc.Fields.Link { Text = "a link", Url = "http://liontrust.co.uk" });
            featuredArticles.SetupGet(fa => fa.Children).Returns(new List<ILink>{ article.Object });
            var result = UrlLink.Map(featuredArticles.Object);
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
        }
    }
}
