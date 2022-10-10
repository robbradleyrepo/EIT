namespace Liontrust.Feature.Search.Tests.SearchOverlay
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Search.SearchOverlay;
    using LionTrust.Foundation.ORM.Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class SearchOverlayControllerShould
    {
        private Mock<IMvcContext> _context;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<IMvcContext>();
        }

        [Test]
        public void ReturnNullWhenNoDatasourceCanBeFound()
        {
            var target = new SearchOverlayController(_context.Object);
            var result = target.Render();

            Assert.IsNull(result);
        }

        [Test]
        public void ReturnANonNullWhenDatasourceIsFound()
        {
            var overlay = new Mock<ISearchOverlay>();
            var searchResultPage = new Mock<IGlassBase>();
            overlay.SetupProperty(m => m.SearchResultsPage, searchResultPage.Object);
            _context.Setup(c => c.GetDataSourceItem<ISearchOverlay>()).Returns(overlay.Object);
            var target = new SearchOverlayController(_context.Object);
            var result = target.Render();

            Assert.NotNull(result);
        }
    }
}
