namespace LionTrust.Feature.Fund.Tests.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Controllers;
    using LionTrust.Feature.Fund.Literature;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class FundLiteratureControllerShould
    {
        private Mock<IMvcContext> _context;

        private Mock<IDocumentRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<IMvcContext>();
            _repository = new Mock<IDocumentRepository>();
        }

        [Test]
        public void ReturnNullWhenNoDatasourceIsFound()
        {
            var target = new FundLiteratureController(_context.Object, _repository.Object);
            var result = target.Render();
            Assert.IsNull(result);
        }
    }
}
