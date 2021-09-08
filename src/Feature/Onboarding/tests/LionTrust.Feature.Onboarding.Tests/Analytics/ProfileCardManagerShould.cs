namespace LionTrust.Feature.Onboarding.Tests.Analytics
{
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Onboarding.Models;
    using LionTrust.Foundation.ORM.Models;
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
        [Ignore("Tech debt might not be possible to test this. Needs someone to look at it.")]
        public void NotThrowAnExceptionWhenTheXmlIsInvalid()
        {
            var profileCard = new Mock<IProfileCard>();
            var config = new Mock<IOnboardingConfiguration>();
            config.Object.Profile = new Mock<IGlassBase>().Object;
            profileCard.SetupGet(p => p.ProfileCardValue).Returns("invalid xml");
            OnboardingHelper.AddPointsFromProfileCard(config.Object, profileCard.Object);
        }
    }
}
