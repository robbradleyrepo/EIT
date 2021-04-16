using LionTrust.Feature.Newsletter.Promo;
using LionTrust.Foundation.Content.Repositories;
using Moq;
using NUnit.Framework;

namespace LionTrust.Feature.NewsletterTests.Promo
{
    [TestFixture]
    public class NewsletterPromoControllerShould
    {
        private Mock<IRenderingRepository> repository;

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IRenderingRepository>();
        }

        [Test]
        public void NotThrowAnExceptionWhenWeTryToRender()
        {
            // Arrange
            var target = new NewsletterPromoController(repository.Object);

            // Act + Assert
            Assert.DoesNotThrow(() => target.Render());
        }
    }
}
