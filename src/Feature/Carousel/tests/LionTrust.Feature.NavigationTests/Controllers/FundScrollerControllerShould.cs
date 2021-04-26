namespace LionTrust.Feature.Carousel.Tests.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Carousel.Controllers;
    using LionTrust.Feature.Carousel.Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class FundScrollerControllerShould
    {
        private Mock<IMvcContext> context;

        [SetUp]
        public void Setup()
        {
            context = new Mock<IMvcContext>(MockBehavior.Strict);
        }

        [Test]
        public void ReturnNullWhenNoDatasourceNotFound()
        {
            context.Setup(c => c.GetDataSourceItem<IFundScroller>()).Returns(() => null);
            var target = new FundScrollerController(context.Object);
            var result = target.Render();
            Assert.IsNull(result);
        }
    }
}
