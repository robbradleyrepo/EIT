namespace LionTrust.Feature.EXM.Personalization
{
    using FuseIT.Sitecore.Personalization.Facets;
    using Sitecore.Modules.EmailCampaign.Core.Personalization;
    using System.Collections.Generic;
    using System.Reflection;

    public class CustomRecipientPropertyTokenMap : DefaultRecipientPropertyTokenMap
    {
        protected static readonly MethodInfo GetEmailPreferencesId = 
            typeof(FacetExtensions).GetMethod(nameof(FacetExtensions.GetEmailPreferencesId), new[] { typeof(S4SInfo) });

        protected static readonly MethodInfo GetOwnerJob =
            typeof(FacetExtensions).GetMethod(nameof(FacetExtensions.GetOwnerJob), new[] { typeof(S4SInfo) });

        protected static readonly MethodInfo GetOwnerName =
            typeof(FacetExtensions).GetMethod(nameof(FacetExtensions.GetOwnerName), new[] { typeof(S4SInfo) });

        protected static readonly MethodInfo GetOwnerPhone =
            typeof(FacetExtensions).GetMethod(nameof(FacetExtensions.GetOwnerPhone), new[] { typeof(S4SInfo) });

        protected static readonly MethodInfo GetOwnerEmail =
            typeof(FacetExtensions).GetMethod(nameof(FacetExtensions.GetOwnerEmail), new[] { typeof(S4SInfo) });

        protected static readonly MethodInfo GetOwnerRegion =
            typeof(FacetExtensions).GetMethod(nameof(FacetExtensions.GetOwnerRegion), new[] { typeof(S4SInfo) });

        static CustomRecipientPropertyTokenMap()
        {
            if (TokenBindings == null)
            {
                TokenBindings = new Dictionary<Token, RecipientPropertyTokenBinding>();
            }
            var emailPreferencesIdTokenBinding = RecipientPropertyTokenBinding.Build<S4SInfo>(new Token(Constants.Tokens.EmailPreferencesId), null, GetEmailPreferencesId);
            TokenBindings.Add(emailPreferencesIdTokenBinding.Token, emailPreferencesIdTokenBinding);

            var ownerJobTokenBinding = RecipientPropertyTokenBinding.Build<S4SInfo>(new Token(Constants.Tokens.OwnerJob), null, GetOwnerJob);
            TokenBindings.Add(ownerJobTokenBinding.Token, ownerJobTokenBinding);

            var ownerNameTokenBinding = RecipientPropertyTokenBinding.Build<S4SInfo>(new Token(Constants.Tokens.OwnerName), null, GetOwnerName);
            TokenBindings.Add(ownerNameTokenBinding.Token, ownerNameTokenBinding);

            var ownerEmailTokenBinding = RecipientPropertyTokenBinding.Build<S4SInfo>(new Token(Constants.Tokens.OwnerEmail), null, GetOwnerEmail);
            TokenBindings.Add(ownerEmailTokenBinding.Token, ownerEmailTokenBinding);

            var ownerPhoneTokenBinding = RecipientPropertyTokenBinding.Build<S4SInfo>(new Token(Constants.Tokens.OwnerPhone), null, GetOwnerPhone);
            TokenBindings.Add(ownerPhoneTokenBinding.Token, ownerPhoneTokenBinding);

            var ownerRegionTokenBinding = RecipientPropertyTokenBinding.Build<S4SInfo>(new Token(Constants.Tokens.OwnerRegion), null, GetOwnerRegion);
            TokenBindings.Add(ownerRegionTokenBinding.Token, ownerRegionTokenBinding);
        }
    }
}
