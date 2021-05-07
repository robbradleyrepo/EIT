namespace LionTrust.Feature.Article.Tests.RelatedArticlesMappers
{
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Article.RelatedArticleMappers;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class RelatedArticleLinkShould
    {
        [Test]
        public void ReturnAnEmpytListWhenThereAreNoArticles()
        {
            var target = new Mock<IFeaturedArticles>();
            target.SetupGet(t => t.Articles).Returns(() => null);
            var result = RelatedArticleLink.Map(target.Object);
            Assert.NotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void ReturnAnEmpytListWhenTheTargetIsNull()
        {
            var result = RelatedArticleLink.Map(null);
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
            var result = RelatedArticleLink.Map(target.Object);
            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
        }
    }
}
