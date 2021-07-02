namespace LionTrust.Foundation.Onboarding.Tests.Helpers
{
    using LionTrust.Foundation.Navigation.Helpers;
    using LionTrust.Foundation.Onboarding.Models;
    using Moq;
    using NUnit.Framework;
    using Sitecore.Abstractions;

    [TestFixture]
    public class OnboardingHelperShould
    {
        private Mock<BaseLog> _log;

        [SetUp]
        public void Setup()
        {
            _log = new Mock<BaseLog>();
        }

        [Test]
        public void NotThrowExceptionsIfThingsAreNull()
        {
            var result = OnboardingHelper.ProfileRoleName(null, _log.Object);
            Assert.IsEmpty(result);
            var configuration = new Mock<IOnboardingConfiguration>();

            result = OnboardingHelper.ProfileRoleName(configuration.Object, _log.Object);
            Assert.IsEmpty(result);

        }
    }
}
