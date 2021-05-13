namespace LionTrust.Feature.Article.Tests.FeaturedArticlesMappers
{
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Article.FeaturedArticleMappers;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using LionTrust.Foundation.Article.Models;

    [TestFixture]
    public class FeaturedArticleLinkShould
    {
        [Test]
        public void ReturnAnEmpytListWhenThereAreNoArticles()
        {
            var target = new Mock<IFeaturedArticles>();
            target.SetupGet(t => t.Articles).Returns(() => null);
            var result = FeaturedArticleLink.Map(target.Object);
            Assert.NotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void ReturnAnEmpytListWhenTheTargetIsNull()
        {
            var result = FeaturedArticleLink.Map(null);
            Assert.NotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void MapTheListWhenThereAreArticles()
        {
            var target = new Mock<IFeaturedArticles>();
            var article = new Mock<IArticle>();
            article.SetupGet(a => a.Title).Returns("a title");
            article.SetupGet(a => a.Url).Returns("http://www.liontrust.co.uk/article-1");
            target.SetupGet(t => t.Articles).Returns(new List<IArticle> { article.Object });
            var result = FeaturedArticleLink.Map(target.Object);
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
        }
    }
}
