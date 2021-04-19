namespace LionTrust.Feature.Navigation.Services
{
    using LionTrust.Feature.Navigation.Models;
    using LionTrust.Feature.Navigation.Services;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class BreadcrumbServiceShould
    {
        [Test]
        public void ReturnAnEmptyListWhenThereAreNoAncestors()
        {
            var data = new Mock<IBreadcrumbDetailsModel>();
            var target = new BreadcrumbService();

            var result = target.GetAncestors(data.Object);
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void ReturnAListWhenThereAreAncestors()
        {
            var data = new Mock<IBreadcrumbDetailsModel>();
            var parent = new Mock<IBreadcrumbDetailsModel>();
            parent.SetupGet(p => p.IncludeInBreadcrumb).Returns(true);
            data.SetupGet(d => d.Parent).Returns(parent.Object);
            var target = new BreadcrumbService();

            var result = target.GetAncestors(data.Object);
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }
    }
}
