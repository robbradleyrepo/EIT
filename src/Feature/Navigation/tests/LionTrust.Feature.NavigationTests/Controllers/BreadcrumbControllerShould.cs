namespace LionTrust.Feature.NavigationTests.Controllers
{
    using Glass.Mapper.Sc.Web;
    using LionTrust.Feature.Navigation.Controllers;
    using LionTrust.Feature.Navigation.Models;
    using LionTrust.Feature.Navigation.Services;
    using Moq;
    using NUnit.Framework;
    using System.Web.Mvc;

    [TestFixture]
    public class BreadcrumbControllerShould
    {
        private Mock<IRequestContext> context;

        private Mock<IBreadcrumbService> breadcrumbService;

        [SetUp]
        public void Setup()
        {
            context = new Mock<IRequestContext>();
            breadcrumbService = new Mock<IBreadcrumbService>();
        }

        [Test]
        public void ReturnNothingIfTheCurrentPageIsNotABreadcrumb()
        {
            var target = new BreadcrumbController(context.Object, breadcrumbService.Object);

            var result = target.Render() as ViewResult;

            Assert.Null(result);
        }

        [Test]
        public void ReturnABreadcrumbViewWithAncestors()
        {
            var currentPage = new Mock<IBreadcrumbDetailsModel>();
            context.Setup(c => c.GetContextItem<IBreadcrumbDetailsModel>()).Returns(currentPage.Object);
            var ancestor = new Mock<IBreadcrumbDetailsModel>();            
            breadcrumbService.Setup(b => b.GetAncestors(It.IsAny<IBreadcrumbDetailsModel>())).Returns(new IBreadcrumbDetailsModel[] { ancestor.Object });
            var target = new BreadcrumbController(context.Object, breadcrumbService.Object);
            
            var result = target.Render() as ViewResult;
            
            Assert.NotNull(result);            
            var model = result.Model as BreadcrumbViewModel;
            Assert.NotNull(model);
            Assert.NotNull(model.CurrentPage);
            Assert.IsNotEmpty(model.Ancestors);
        }
    }
}
