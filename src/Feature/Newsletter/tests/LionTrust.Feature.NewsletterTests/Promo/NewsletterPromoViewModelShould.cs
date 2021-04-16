using LionTrust.Feature.Newsletter.Promo;
using Moq;
using NUnit.Framework;
using System;

namespace LionTrust.Feature.NewsletterTests.Promo
{
    [TestFixture]
    public class NewsletterPromoViewModelShould
    {
        private Mock<INewsletterPromoModel> data;

        [SetUp]
        public void Setup()
        {
            data = new Mock<INewsletterPromoModel>();
        }

        [Test]
        public void ExtractTheGoalIdFromTheGoal()
        {
            // Arrange
            data.SetupGet(d => d.Goal).Returns("B81359EA-DFDA-4A80-BDAC-6F55B9F2AF64");
            var target = new NewsletterPromoViewModel(data.Object);

            // Act

            // Assert
            Assert.AreNotEqual(Guid.Empty, target.GoalId);
        }

        [Test]
        public void ExtractTheBackgroundImageUrlFromTheBackgroundImage()
        {
            // Arrange
            data.SetupGet(d => d.BackgroundImage).Returns(new Glass.Mapper.Sc.Fields.Image { Src = "http://www.google.co.uk/imge.png" });
            var target = new NewsletterPromoViewModel(data.Object);

            // Act

            // Assert
            Assert.IsNotEmpty(target.BackgroundImageUrl);
        }
    }
}
