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

        protected static readonly MethodInfo GetOwnerTitle =
            typeof(FacetExtensions).GetMethod(nameof(FacetExtensions.GetOwnerTitle), new[] { typeof(EmailAddressList) });

        protected static readonly MethodInfo GetOwnerName =
            typeof(FacetExtensions).GetMethod(nameof(FacetExtensions.GetOwnerName), new[] { typeof(EmailAddressList) });

        protected static readonly MethodInfo GetOwnerPhone =
            typeof(FacetExtensions).GetMethod(nameof(FacetExtensions.GetOwnerPhone), new[] { typeof(EmailAddressList) });

        protected static readonly MethodInfo GetOwnerEmail =
            typeof(FacetExtensions).GetMethod(nameof(FacetExtensions.GetOwnerEmail), new[] { typeof(EmailAddressList) });

        protected static readonly MethodInfo GetOwnerRegion =
            typeof(FacetExtensions).GetMethod(nameof(FacetExtensions.GetOwnerRegion), new[] { typeof(EmailAddressList) });

        static CustomRecipientPropertyTokenMap()
        {
            if (TokenBindings == null)
            {
                TokenBindings = new Dictionary<Token, RecipientPropertyTokenBinding>();
            }
            var emailPreferencesIdTokenBinding = RecipientPropertyTokenBinding.Build<EmailAddressList>(new Token(FacetKeys.EmailPreferencesId), (Expression<Func<EmailAddressList, object>>)null, CustomRecipientPropertyTokenMap.GetEmailPreferencesId);
            TokenBindings.Add(emailPreferencesIdTokenBinding.Token, emailPreferencesIdTokenBinding);

            var ownerTitleTokenBinding = RecipientPropertyTokenBinding.Build<EmailAddressList>(new Token(FacetKeys.OwnerTitle), (Expression<Func<EmailAddressList, object>>)null, CustomRecipientPropertyTokenMap.GetOwnerTitle);
            TokenBindings.Add(ownerTitleTokenBinding.Token, ownerTitleTokenBinding);

            var ownerNameTokenBinding = RecipientPropertyTokenBinding.Build<EmailAddressList>(new Token(FacetKeys.OwnerName), (Expression<Func<EmailAddressList, object>>)null, CustomRecipientPropertyTokenMap.GetOwnerName);
            TokenBindings.Add(ownerNameTokenBinding.Token, ownerNameTokenBinding);

            var ownerEmailTokenBinding = RecipientPropertyTokenBinding.Build<EmailAddressList>(new Token(FacetKeys.OwnerEmail), (Expression<Func<EmailAddressList, object>>)null, CustomRecipientPropertyTokenMap.GetOwnerEmail);
            TokenBindings.Add(ownerEmailTokenBinding.Token, ownerEmailTokenBinding);

            var ownerPhoneTokenBinding = RecipientPropertyTokenBinding.Build<EmailAddressList>(new Token(FacetKeys.OwnerPhone), (Expression<Func<EmailAddressList, object>>)null, CustomRecipientPropertyTokenMap.GetOwnerPhone);
            TokenBindings.Add(ownerPhoneTokenBinding.Token, ownerPhoneTokenBinding);

            var ownerRegionTokenBinding = RecipientPropertyTokenBinding.Build<EmailAddressList>(new Token(FacetKeys.OwnerRegion), (Expression<Func<EmailAddressList, object>>)null, CustomRecipientPropertyTokenMap.GetOwnerRegion);
            TokenBindings.Add(ownerRegionTokenBinding.Token, ownerRegionTokenBinding);
        }
    }
}
