namespace LionTrust.Feature.MyPreferences.Services
{
    using FuseIT.Sitecore.SalesforceConnector;

    internal class S4SSessionSingleton : SalesforceSessionSingleton
    {
        private S4SSessionSingleton()
        : base(null, "S4SConnString")
        {
        }

        /// 
        /// Get the singleton instance of the Salesforce Session.
        /// 
        public static SalesforceSession SessionInstance
        {
            get
            {
                S4SSessionSingleton singleton = new S4SSessionSingleton();
                return singleton.Instance;
            }
            set
            {
                // This is only required if you want code outside the singleton to be able to construct the session
                S4SSessionSingleton singleton = new S4SSessionSingleton();
                singleton.SetNewInstance(value);
            }
        }
    }
}
