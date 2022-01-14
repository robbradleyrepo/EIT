namespace LionTrust.Feature.EXM.Personalization
{
    using Sitecore.Modules.EmailCampaign.Core.Personalization;
    using Sitecore.XConnect.Collection.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class CustomRecipientPropertyTokenMap : DefaultRecipientPropertyTokenMap
    {
        protected static readonly MethodInfo GetEmailPreferencesId = 
            typeof(FacetExtensions).GetMethod(nameof(FacetExtensions.GetEmailPreferencesId), new[] { typeof(EmailAddressList) });

        static CustomRecipientPropertyTokenMap()
        {
            if (TokenBindings == null)
            {
                TokenBindings = new Dictionary<Token, RecipientPropertyTokenBinding>();
            }
            RecipientPropertyTokenBinding customTokenBinding = RecipientPropertyTokenBinding.Build<EmailAddressList>(new Token(FacetKeys.EmailPreferencesId), (Expression<Func<EmailAddressList, object>>)null, CustomRecipientPropertyTokenMap.GetEmailPreferencesId);
            TokenBindings.Add(customTokenBinding.Token, customTokenBinding);
        }
    }
}
