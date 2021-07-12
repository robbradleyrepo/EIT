namespace LionTrust.Feature.Onboarding.Tests.Analytics
{
    using LionTrust.Feature.Onboarding.Analytics;
    using LionTrust.Foundation.Onboarding.Models;
    using Moq;
    using NUnit.Framework;
    using Sitecore.Abstractions;
    using Sitecore.Analytics.Tracking;

    [TestFixture]
    public class ProfileCardManagerShould
    {
        private Mock<BaseLog> _log;

        [SetUp]
        public void Setup()
        {
            _log = new Mock<BaseLog>();
        }

        [Test]
        public void NotThrowAnExceptionWhenTheXmlIsInvalid()
        {
            var target = new ProfileCardManager(_log.Object);
            var profileCard = new Mock<IProfileCard>();
            profileCard.SetupGet(p => p.ProfileCardValue).Returns("invalid xml");
            var profile = new Profile("test-profile");
            target.AddPointsFromProfileCard(profileCard.Object, profile);
        }
    }
}
