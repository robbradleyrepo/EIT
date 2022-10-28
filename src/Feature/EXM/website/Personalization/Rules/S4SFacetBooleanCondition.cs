namespace LionTrust.Feature.EXM.Personalization.Rules
{
    using FuseIT.Sitecore.Personalization.Extensions;
    using FuseIT.Sitecore.Personalization.Facets;
    using FuseIT.Sitecore.Salesforce;
    using Sitecore.Analytics;
    using Sitecore.Data;
    using Sitecore.Diagnostics;
    using Sitecore.Rules;
    using Sitecore.Rules.Conditions;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Client;
    using Sitecore.XConnect.Client.Configuration;
    using ContactConstants = Foundation.Contact.Constants;

    public class S4SFacetBooleanCondition<T> : WhenCondition<T> where T : RuleContext
    {
        private const string Name = "Name";

        public string ItemId { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Logging.Info(this, "FacetDictionaryValueCondition Execute()");
            Assert.IsNotNull(Tracker.Current, "Tracker.Current is not initialized");
            Assert.IsNotNull(Tracker.Current.Contact, "Tracker.Current.Contact is not initialized");

            if (string.IsNullOrEmpty(ItemId))
            {
                Logging.Info(this, "FacetDictionaryValueCondition Rule condition Item Id is null");
                return false;
            }

            var itemId = new ID(ItemId);
            var key = ruleContext.Item.Database.GetItem(itemId).Fields[Name].Value;

            Logging.DebugFormat(this, "FacetDictionaryValueCondition Facet name is {0} from item {1}", key, itemId);

            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                var reference = new IdentifiedContactReference(ContactConstants.Identifier.XdbTracker, Tracker.Current.Contact.ContactId.ToString("N"));
                var expandOptions = new ContactExpandOptions(S4SInfo.DefaultFacetKey);
                var contact = client.Get(reference, expandOptions);

                if (contact == null)
                {
                    Logging.Info(this, "FacetDictionaryValueCondition Rule contact not found in xDB. Identifier Source: " + Sitecore.XConnect.Constants.AliasIdentifierSource + ", Identifier: " + Tracker.Current.Contact.ContactId.ToString("D"));
                    return false;
                }

                var s4Sinfo = contact.S4SInfo();
                if (s4Sinfo?.Fields != null && s4Sinfo.Fields.TryGetValue(key, out string field) && bool.TryParse(field, out var fieldValue))
                {
                    Logging.DebugFormat(this, "FacetDictionaryValueCondition Facet key found a match Rule Key {0}", key);
                    return fieldValue;
                }
            }

            return false;
        }
    }
}
