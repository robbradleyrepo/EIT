using Glass.Mapper.Sc.Web;
using LionTrust.Feature.Breadcrumb;
using LionTrust.Feature.Breadcrumb.Models;
using LionTrust.Feature.Breadcrumb.Services;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace LionTrust.Feature.BreadcrumbTests
{
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
