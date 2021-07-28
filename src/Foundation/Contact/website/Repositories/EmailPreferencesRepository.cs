namespace LionTrust.Foundation.Contact.Repositories
{
    using LionTrust.Foundation.Contact.Models.EditEmailPreferences;
    using LionTrust.Foundation.Contact.Services;
    using Sitecore.Links;
    using System;
    using System.Web;

    public class EmailPreferencesRepository : BaseRepository, IEmailPreferencesRepository
    {
        private readonly IApplicationCacheRepository _applicationCacheRepository;
        private const string SFContactCountryListCacheKey = "salesforce-contact-country-list";
        private const string SFLeadCountryListCacheKey = "salesforce-lead-country-list";

        public EmailPreferencesRepository(IEntityFactory entityFactory, IApplicationCacheRepository applicationCacheRepository) : base(entityFactory)
        {
            _applicationCacheRepository = applicationCacheRepository;
        }
        public EmailPreferences GetEmailPreferences(string sfContactId, string sfRandomGUID, bool isContact)
        {
            var sfEntityUtility = new SFEntityUtility();
            return sfEntityUtility.GetSFEmailPreferences(sfContactId, sfRandomGUID, isContact);
        }

        public bool SaveEmailPreferneces(EmailPreferences emailPreferences)
        {
            var sfEntityUtility = new SFEntityUtility();
            return sfEntityUtility.SaveEmailPreferences(emailPreferences);
        }

        public RegisterdUserWithEmailDetails SaveNonProfUserAsSFLead(NonProfessionalUser nonProfUserViewModel)
        {
            var sfEntityUtility = new SFEntityUtility();
            var savedUserEmailDetails = sfEntityUtility.SaveNonProfUserAsSFLead(nonProfUserViewModel);
            if (savedUserEmailDetails != null)
            {
                if (savedUserEmailDetails.IsUserExists)
                {
                    return new RegisterdUserWithEmailDetails { IsUserExists = savedUserEmailDetails.IsUserExists };
                }

                var emailTemplateId = (nonProfUserViewModel.IsUKResident) ? Constants.ItemIds.Content.Global.EmailTemplates.EditEmailPrefEmailTemplateForUkResidents : Constants.ItemIds.Content.Global.EmailTemplates.EditEmailPrefEmailTemplateForNonUkResidents;
                var emailItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(emailTemplateId));
                if (emailItem != null)
                {
                    var emailItemViewModel = EntityFactory.Build<EditEmailPrefEmailTemplateViewModel>(emailItem);

                    //Generate email body
                    var emailMessageBody = emailItemViewModel.Message;
                    emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.FullNameToken, savedUserEmailDetails.FullName);
                    emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.EditPrefLinkToken, savedUserEmailDetails.EditEmailPrefLink);
                    emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.SiteURLToken, string.Format("https://{0}", HttpContext.Current.Request.Url.Host));

                    var returnObj = new RegisterdUserWithEmailDetails
                    {
                        IsUserExists = savedUserEmailDetails.IsUserExists,
                        FromAddress = emailItemViewModel.FromAddress,
                        FromDisplyName = emailItemViewModel.FromDisplyName,
                        ToAddresses = savedUserEmailDetails.EmailAddress,
                        Subject = emailItemViewModel.Subject,
                        Message = emailMessageBody
                    };

                    return returnObj;
                }
            }

            return null;
        }

        public RegisterdUserWithEmailDetails SaveProfUserAsSFContact(ProfessionalUser profUserViewModel)
        {
            var sfEntityUtility = new SFEntityUtility();
            var savedUserEmailDetails = sfEntityUtility.SaveProfUserAsSFContact(profUserViewModel);
            if (savedUserEmailDetails != null)
            {
                if (savedUserEmailDetails.IsUserExists)
                {
                    return new RegisterdUserWithEmailDetails { IsUserExists = savedUserEmailDetails.IsUserExists };
                }

                var emailTemplateId = (profUserViewModel.IsUKResident) ? Constants.ItemIds.Content.Global.EmailTemplates.EditEmailPrefEmailTemplateForUkResidents : Constants.ItemIds.Content.Global.EmailTemplates.EditEmailPrefEmailTemplateForNonUkResidents;
                var emailItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(emailTemplateId));
                if (emailItem != null)
                {
                    var emailItemViewModel = EntityFactory.Build<EditEmailPrefEmailTemplateViewModel>(emailItem);

                    //Generate email body
                    var emailMessageBody = emailItemViewModel.Message;
                    emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.FullNameToken, savedUserEmailDetails.FullName);
                    emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.EditPrefLinkToken, savedUserEmailDetails.EditEmailPrefLink);
                    emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.SiteURLToken, string.Format("https://{0}", HttpContext.Current.Request.Url.Host));

                    var returnObj = new RegisterdUserWithEmailDetails
                    {
                        IsUserExists = savedUserEmailDetails.IsUserExists,
                        FromAddress = emailItemViewModel.FromAddress,
                        FromDisplyName = emailItemViewModel.FromDisplyName,
                        ToAddresses = savedUserEmailDetails.EmailAddress,
                        Subject = emailItemViewModel.Subject,
                        Message = emailMessageBody
                    };

                    return returnObj;
                }
            }

            return null;
        }

        public SFCountryListViewModel GetCountryListFromSF(bool isFromContact, string defaultOptionText)
        {
            var sfEntityUtility = new SFEntityUtility();
            var cacheKey = (isFromContact) ? SFContactCountryListCacheKey : SFLeadCountryListCacheKey;
            var countryListViewModel = _applicationCacheRepository.Read<SFCountryListViewModel>(cacheKey);
            //If not in cache, retrieve directly from SF
            if (countryListViewModel == default(SFCountryListViewModel))
            {
                countryListViewModel = sfEntityUtility.GetCountryListFromSF(isFromContact, defaultOptionText);
                _applicationCacheRepository.Write(cacheKey, countryListViewModel, new TimeSpan(6, 0, 0));
            }

            return countryListViewModel;
        }

        public ResendEmailPrefEmailDetails GetEmailDetailsForResendEmailPrefLink(string email, bool isContact)
        {
            var sfEntityUtility = new SFEntityUtility();
            var userDetails = sfEntityUtility.GetEmailDetailsForResendEmailPrefLink(email, isContact);

            if (userDetails != null)
            {
                var emailItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(Constants.ItemIds.Content.Global.EmailTemplates.ResendEditEmailPrefLinkEmailTemplate));
                if (emailItem != null)
                {
                    var emailItemViewModel = EntityFactory.Build<EditEmailPrefEmailTemplateViewModel>(emailItem);

                    //Generate email body
                    var emailMessageBody = emailItemViewModel.Message;
                    emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.FullNameToken, userDetails.FullName);
                    emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.EditPrefLinkToken, userDetails.EditEmailPrefLink);
                    emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.SiteURLToken, string.Format("https://{0}", HttpContext.Current.Request.Url.Host));

                    var returnObj = new ResendEmailPrefEmailDetails
                    {
                        FromAddress = emailItemViewModel.FromAddress,
                        FromDisplyName = emailItemViewModel.FromDisplyName,
                        ToAddresses = email,
                        Subject = emailItemViewModel.Subject,
                        Message = emailMessageBody
                    };

                    return returnObj;
                }
            }

            return null;
        }

        public ResendEmailPrefLinkUIDetails GetUIDetailsForResendEmailPrefEmail(bool isSuccess)
        {
            var genericTextModuleItemId = (isSuccess) ? Constants.ItemIds.Content.Global.EmailPreferences.ResendEmailPrefLinkSuccess : Constants.ItemIds.Content.Global.EmailPreferences.ResendEmailPrefLinkFailed;
            var registerNonProfUserLabelItem = GetItem(genericTextModuleItemId);
            return EntityFactory.Build<ResendEmailPrefLinkUIDetails>(registerNonProfUserLabelItem);
        }

        public string GetRedirectUrlAfterEditing(bool isSuccess)
        {
            var redirectPageId = (isSuccess) ? Constants.ItemIds.Content.Global.EmailPreferences.EditEmailPreferecesSuccessPage : Constants.ItemIds.Content.Global.EmailPreferences.EditEmailPreferencesFailurePage;
            return LinkManager.GetItemUrl(GetItem(redirectPageId));
        }
    }
}
