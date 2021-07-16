namespace LionTrust.Feature.Onboarding.Analytics
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Abstractions;
    using Sitecore.Analytics.Tracking;
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Linq;
    using static LionTrust.Feature.Onboarding.Constants;

    [Service(ServiceType = typeof(IProfileCardManager), Lifetime = Lifetime.Singleton)]
    public class ProfileCardManager : IProfileCardManager
    {
        private readonly BaseLog _log;

        public ProfileCardManager(BaseLog log)
        {
            this._log = log;
        }

        public void AddPointsFromProfileCard(IProfileCard profileCard, Profile profile)
        {
            var scores = new Dictionary<string, double>();

            if (profileCard != null && !string.IsNullOrEmpty(profileCard.ProfileCardValue))
            {
                try
                {
                    var xmlData = XDocument.Parse(profileCard.ProfileCardValue);
                    var xmlDoc = xmlData;

                    if (xmlDoc != null)
                    {
                        var parentNode = xmlDoc.Elements(Analytics.ProfileCardValueKey_XmlElementName);

                        if (parentNode != null)
                        {
                            foreach (var childrenNode in parentNode)
                            {
                                if (childrenNode.HasAttributes
                                    && childrenNode.Attribute(Analytics.ProfileCardValueName_XmlAttribute) != null && !string.IsNullOrWhiteSpace(childrenNode.Attribute(Analytics.ProfileCardValueName_XmlAttribute).Value)
                                    && childrenNode.Attribute(Analytics.ProfileCardValueValue_XmlAttribute) != null && !string.IsNullOrWhiteSpace(childrenNode.Attribute(Analytics.ProfileCardValueValue_XmlAttribute).Value))
                                {
                                    scores.Add(childrenNode.Attribute(Analytics.ProfileCardValueName_XmlAttribute).Value, Convert.ToDouble(childrenNode.Attribute(Analytics.ProfileCardValueValue_XmlAttribute).Value));
                                }
                            }

                            profile.Score(scores);

                            // update the pattern based on the scores you updated - this is supposed to be called from Score as well
                            // but doesn't always update unless you call it explicitly
                            profile.UpdatePattern();
                        }
                    }
                }
                catch (XmlException ex)
                {
                    _log.Error($"Xml for profile card is invalid. Value is {profileCard.ProfileCardValue}", ex, this);
                }
            }
        }
    }
}