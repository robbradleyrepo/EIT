namespace LionTrust.Foundation.Contact.Services
{
    using FuseIT.Sitecore.SalesforceConnector.Entities;
    using LionTrust.Foundation.Contact.Enums;
    using LionTrust.Foundation.Contact.Models;
    using System;
    using System.Collections.Generic;

    public interface ISFEntityUtility
    {
        EmailPreferences GetSFEmailPreferences(Context context, bool loadFundsForMultiAssetProcesse = false);

        List<SFProcess> GetSFProcessList(Context context = null, bool loadFundsForMultiAssetProcesse = false);

        /// <summary>
        /// Save email preferences in Salesforce
        /// </summary>
        /// <param name="emailPreferences"></param>
        /// <returns></returns>
        bool SaveEmailPreferences(Context context);

        /// <summary>
        /// Save non professional investor as a SF Lead
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        RegisterdUserReturnViewModel SaveNonProfUserAsSFLead(NonProfessionalUser nonProfUser, string PreferencesUrl, string fundDashboardUrl);

        /// <summary>
        /// Save professional investor as a SF Contact
        /// </summary>
        /// <param name="profUser"></param>
        /// <returns></returns>
        RegisterdUserReturnViewModel SaveProfUserAsSFContact(ProfessionalUser profUser, string preferencesUrl, string fundDashboardUrl);

        /// <summary>
        /// Save hard bounced in Salesforce
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool SaveHardBounced(EntityBase entity);

        /// <summary>
        /// Get details from SF for resending email pref email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isContact"></param>
        /// <returns></returns>
        ResendEmailPrefUserDetails GetEmailDetailsForResendEmailPrefLink(string email, bool isContact, string preferencesUrl, string fundDashboardUrl);

        /// <summary>
        /// Get country list from SF
        /// </summary>
        /// <param name="isFromContact"></param>
        /// <param name="defaultOptionText"></param>
        /// <returns></returns>
        SFCountryListViewModel GetCountryListFromSF(bool isFromContact, string defaultOptionText);

        /// <summary>
        /// Identify visitor by email address in Sitecore and get visitor id
        /// </summary>
        /// <param name="emailAddress"></param>
        string IdentifyVisitorAndGetVisitorId(string emailAddress);

        /// <summary>
        /// Get email preferences id by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        string GetEmailPreferencesIdByEmail(string email);

        /// <summary>
        /// Get owner title by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        string GetOwnerTitleByEmail(string email);

        /// <summary>
        /// Get owner name by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        string GetOwnerNameByEmail(string email);

        /// <summary>
        /// Get owner email by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        string GetOwnerEmailByEmail(string email);

        /// <summary>
        /// Get owner title by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        string GetOwnerPhoneByEmail(string email);

        /// <summary>
        /// Get owner region by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        string GetOwnerRegionByEmail(string email);

        /// <summary>
        /// Is unsubscribed
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsUnsubscribed(string email);

        /// <summary>
        /// Is unsubscribed or hard bounced
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsUnsubscribedOrHardBounced(string email);

        /// <summary>
        /// Get salesforce entity by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        EntityBase GetEntityByEmail(string email);

        /// <summary>
        /// Get identifier by entity
        /// </summary>
        /// <param name="sfEntity"></param>
        /// <returns></returns>
        string GetIdentifier(EntityBase sfEntity);

        void UpdateOrInsertEntities(List<GenericSalesforceEntity> entities, string entityType, string externalFieldId);

        /// <summary>
        /// Get entity with updated score
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entityType"></param>
        /// <param name="scorePoints"></param>
        /// <returns></returns>
        GenericSalesforceEntity GetEntityWithUpdatedScore(string entityId, string entityType, int scorePoints);

        /// <summary>
        /// Generates engagement history object
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="sfEntityId"></param>
        /// <param name="sfCampaignId"></param>
        /// <param name="entityType"></param>
        /// <param name="interactionType"></param>
        /// <param name="contactList"></param>
        /// <param name="email"></param>
        /// <param name="messageLink"></param>
        /// <param name="link"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        GenericSalesforceEntity GenerateEngagementHistory(Guid campaignId, string sfEntityId, string sfCampaignId, EntityType entityType, InteractionType interactionType, string contactList, string email, string messageLink, string link, DateTime date);
    }
}
