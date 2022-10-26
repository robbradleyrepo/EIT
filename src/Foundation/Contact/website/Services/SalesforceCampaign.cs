using FuseIT.S4S.SitecoreSalesforceListBuilder.Models;
using FuseIT.S4S.WebToSalesforce.Connection;
using FuseIT.Sitecore.Personalization.Facets;
using FuseIT.Sitecore.Salesforce;
using FuseIT.Sitecore.SalesforceConnector.Entities;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionTrust.Foundation.Contact.Services
{
    public class SalesforceCampaign : FuseIT.S4S.SitecoreSalesforceListBuilder.SalesforceCampaigns.SalesforceCampaign, ISalesforceCampaign
    {
        public new async Task<int> CreateSitecoreContactsFromSFCampaignMembers(string connStringName, List<CampaignMember> campaignMembers, Guid sitecoreContactListId)
        {
            var contactList = new List<Sitecore.XConnect.Contact>();
            var source = new List<string>();
            
            var salesforceOrganizationId = SalesforceSessionCache.GetSalesforceSession(connStringName).SalesforceOrganizationId;
            if (!string.IsNullOrEmpty(salesforceOrganizationId))
            {
                using (List<CampaignMember>.Enumerator enumerator = campaignMembers.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;
                        try
                        {
                            var firstName = string.Empty;
                            var lastName = string.Empty;
                            var emailAddress = string.Empty;
                            var recordIdentifier = string.Empty;
                            FuseIT.Sitecore.SalesforceConnector.Entities.Contact sfContact = null;
                            Lead sfLead = null;

                            if (current.Contact != null)
                            {
                                if (current.Contact.Id != null && !string.IsNullOrEmpty(current.Contact.Id))
                                {
                                    recordIdentifier = current.Contact.Id;
                                }

                                if (current.Contact.Email != null && !string.IsNullOrEmpty(current.Contact.Email))
                                {
                                    emailAddress = current.Contact.Email;
                                }

                                if (current.Contact.FirstName != null && !string.IsNullOrEmpty(current.Contact.FirstName))
                                {
                                    firstName = current.Contact.FirstName;
                                }

                                if (current.Contact.LastName != null && !string.IsNullOrEmpty(current.Contact.LastName))
                                {
                                    lastName = current.Contact.LastName;
                                }

                                sfContact = current.Contact;
                            }
                            else if (current.Lead != null)
                            {
                                if (current.Lead.Id != null && !string.IsNullOrEmpty(current.Lead.Id))
                                {
                                    recordIdentifier = current.Lead.Id;
                                }

                                if (current.Lead.Email != null && !string.IsNullOrEmpty(current.Lead.Email))
                                {
                                    emailAddress = current.Lead.Email;
                                }

                                if (current.Lead.FirstName != null)
                                {
                                    firstName = current.Lead.FirstName;
                                }

                                if (current.Lead.LastName != null && !string.IsNullOrEmpty(current.Lead.LastName))
                                {
                                    lastName = current.Lead.LastName;
                                }

                                sfLead = current.Lead;
                            }
                            else
                            {
                                continue;
                            }

                            if (!string.IsNullOrEmpty(emailAddress) && !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(recordIdentifier))
                            {
                                var contactRecordIdentifier = FuseIT.S4S.SitecoreSalesforceListBuilder.SitecoreUtility.SitecoreContactUtility.GenerateContactRecordIdentifier(salesforceOrganizationId, recordIdentifier);
                                if (source.Any(x => x == contactRecordIdentifier))
                                {
                                    continue;
                                }

                                using (var client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                                {
                                    var contact = await CreateContact(contactRecordIdentifier, firstName, lastName, emailAddress, current.Id, sfContact, sfLead, sitecoreContactListId);
                                    if (contact != null)
                                    {
                                        contactList.Add(contact);
                                        source.Add(contactRecordIdentifier);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.Error(this, string.Format("Error has occured trying to create/update the contact with Id={0}.", current.Id), ex);
                        }
                    }
                }
            }

            return contactList.Count;
        }

        private async Task<Sitecore.XConnect.Contact> CreateContact(string identifier, string firstName, string lastName, string emailAddress, string campaignMemberId, FuseIT.Sitecore.SalesforceConnector.Entities.Contact sfContact, Lead sfLead, Guid sitecoreContactListId)
        {
            using (var client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
            {
                try
                {
                    var contactReference = new IdentifiedContactReference(Constants.Identifier.S4SLB, identifier);
                    var contact = await GetContact(client, identifier, emailAddress);
                    if (contact == null)
                    {
                        contact = new Sitecore.XConnect.Contact(new ContactIdentifier[2]
                                    {
                                        new ContactIdentifier(Constants.Identifier.S4SLB, identifier, ContactIdentifierType.Known),
                                        new ContactIdentifier(Constants.Identifier.S4S, emailAddress, ContactIdentifierType.Known),
                                    });

                        client.AddContact(contact);
                    }
                    if (contact != null)
                    {
                        if (!contact.Identifiers.Any(x => x.Source == Constants.Identifier.S4SLB))
                        {
                            client.AddContactIdentifier(contact, new ContactIdentifier(Constants.Identifier.S4SLB, emailAddress, ContactIdentifierType.Known));
                        }

                        if (!contact.Identifiers.Any(x => x.Source == Constants.Identifier.S4S))
                        {
                            client.AddContactIdentifier(contact, new ContactIdentifier(Constants.Identifier.S4S, identifier, ContactIdentifierType.Known));
                        }

                        string preferredKey = Constants.EmailAddressList.PreferredKey;
                        var emailsFacet = contact.Emails();
                        if (emailsFacet != null)
                        {
                            var bounceCount = emailsFacet.PreferredEmail.BounceCount;
                            emailsFacet.PreferredEmail = new EmailAddress(emailAddress, true)
                            {
                                BounceCount = bounceCount
                            };
                            emailsFacet.PreferredKey = preferredKey;
                            client.SetFacet(contact, EmailAddressList.DefaultFacetKey, emailsFacet);
                        }
                        else
                        {
                            var data = new EmailAddressList(new EmailAddress(emailAddress, true), preferredKey);
                            client.SetFacet(contact, EmailAddressList.DefaultFacetKey, data);
                        }
                        
                        var personalFacet = contact.Personal();
                        if (personalFacet != null)
                        {
                            personalFacet.FirstName = firstName;
                            personalFacet.LastName = lastName;
                            client.SetFacet(contact, PersonalInformation.DefaultFacetKey, personalFacet);
                        }
                        else
                        {
                            var data = new PersonalInformation()
                            {
                                FirstName = firstName,
                                LastName = lastName
                            };
                            client.SetFacet(contact, PersonalInformation.DefaultFacetKey, data);
                        }

                        var listSubscriptionsFacet = contact.ListSubscriptions();
                        if (listSubscriptionsFacet != null)
                        {
                            if (listSubscriptionsFacet.Subscriptions.Find(x => x.ListDefinitionId == sitecoreContactListId) == null)
                            {
                                var listSubscription = new ContactListSubscription(DateTime.UtcNow, true, sitecoreContactListId);
                                listSubscriptionsFacet.Subscriptions.Add(listSubscription);
                                client.SetFacet(contact, ListSubscriptions.DefaultFacetKey, listSubscriptionsFacet);
                            }
                        }
                        else
                        {
                            var listSubscription = new ContactListSubscription(DateTime.UtcNow, true, sitecoreContactListId);
                            client.SetFacet(contact, ListSubscriptions.DefaultFacetKey, new ListSubscriptions()
                            {
                                Subscriptions = 
                                {
                                    listSubscription
                                }
                            });
                        }
                        if (!string.IsNullOrEmpty(campaignMemberId))
                        {
                            var s4sInfoFacet = contact.GetFacet<S4SInfo>(S4SInfo.DefaultFacetKey);
                            if (s4sInfoFacet != null)
                            {
                                if (!s4sInfoFacet.AvailableCampaignIds.Contains(campaignMemberId))
                                {
                                    s4sInfoFacet.AvailableCampaignIds.Add(campaignMemberId);
                                    client.SetFacet(contact, S4SInfo.DefaultFacetKey, s4sInfoFacet);
                                }
                            }
                            else
                            {
                                var data = new S4SInfo()
                                {
                                    AvailableCampaignIds = new List<string>()
                                            {
                                              campaignMemberId
                                            }
                                };

                                client.SetFacet(contact, S4SInfo.DefaultFacetKey, data);
                            }
                        }
                        client.Submit();

                        MapS4SFields(client, contact, sfContact, sfLead);
                    }
                    return contact;
                }
                catch (Exception ex)
                {
                    Logging.Error(this, string.Format("Error occured trying to create a contact with identifier = {0} and email = {1}", identifier, emailAddress), ex);
                    return null;
                }
            }
        }

        private async Task<Sitecore.XConnect.Contact> GetContact(XConnectClient client, string identifier, string emailAddress)
        {
            var contactReference = new IdentifiedContactReference(Constants.Identifier.S4SLB, identifier);
            var contact = await client.GetContactAsync(contactReference, new ContactExpandOptions(EmailAddressList.DefaultFacetKey, PersonalInformation.DefaultFacetKey, ListSubscriptions.DefaultFacetKey, S4SInfo.DefaultFacetKey));

            if (contact == null)
            {
                contactReference = new IdentifiedContactReference(Constants.Identifier.S4S, emailAddress);
                contact = await client.GetContactAsync(contactReference, new ContactExpandOptions(EmailAddressList.DefaultFacetKey, PersonalInformation.DefaultFacetKey, ListSubscriptions.DefaultFacetKey, S4SInfo.DefaultFacetKey));
            }

            return contact;
        }

        private void MapS4SFields(
            XConnectClient client,
            Sitecore.XConnect.Contact xdbContact,
            FuseIT.Sitecore.SalesforceConnector.Entities.Contact sfContact,
            Lead lead)
        {
            try
            {
                var sfEntityUtility = new FuseIT.S4S.SitecoreSalesforceListBuilder.SalesforceUtility.SFEntityUtility();
                var mappingItemViewModelList = new List<FacetMappingItemViewModel>();

                if (sfContact != null)
                {
                    mappingItemViewModelList = sfEntityUtility.GetSFContactFieldValueList(sfContact);
                }
                else if (lead != null)
                {
                    mappingItemViewModelList = sfEntityUtility.GetSFLeadFieldValueList(lead);
                }

                if (mappingItemViewModelList.Count > 0)
                {
                    var data = xdbContact.GetFacet<S4SInfo>(S4SInfo.DefaultFacetKey);
                    using (var enumerator = mappingItemViewModelList.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            var current = enumerator.Current;
                            if (data != null)
                            {
                                if (data.Fields.ContainsKey(current.SFFieldName))
                                {
                                    if (!string.IsNullOrEmpty(current.SFFieldValue))
                                    {
                                        data.Fields[current.SFFieldName] = current.SFFieldValue;
                                    }
                                    else
                                    {
                                        data.Fields.Remove(current.SFFieldName);
                                    }
                                }
                                else if (!string.IsNullOrEmpty(current.SFFieldValue))
                                {
                                    data.Fields.Add(current.SFFieldName, current.SFFieldValue);
                                }
                            }
                            else if (!string.IsNullOrEmpty(current.SFFieldValue))
                            {
                                data = new S4SInfo()
                                {
                                    Fields = new Dictionary<string, string>()
                                            {
                                                {
                                                current.SFFieldName,
                                                current.SFFieldValue
                                                }
                                            }
                                };
                            }
                        }
                    }
                    client.SetFacet(xdbContact, S4SInfo.DefaultFacetKey, data);
                    client.Submit();
                }
                else
                {
                    Logging.Info(this, "There is no S4S fields to be mapped.");
                }
            }
            catch (Exception ex)
            {
                Logging.Error(this, "Error occured trying to map S4S fields", ex);
            }
        }
    }
}