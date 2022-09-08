using FuseIT.Sitecore.Personalization.Facets;
using LionTrust.Feature.EXM.Models;
using LionTrust.Foundation.Contact.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace LionTrust.Feature.EXM.Helpers.Implementations
{
    public static class SFEntityHelper
    {
        public static string GetFieldValue(S4SInfo info, string key, string userKey = null)
        {
            if (userKey == null)
            {
                return info.Fields.ContainsKey(key) ? info.Fields[key] : null;
            }

            var sfEntityUtility = ServiceLocator.ServiceProvider.GetService<ISFEntityUtility>();

            if (sfEntityUtility.IsContact(info.Fields[Foundation.Contact.Constants.SF_IdField]))
            {
                return info.Fields.ContainsKey(key) ? info.Fields[key] : null;
            }
            else
            {
                var ownerId = info.Fields[Foundation.Contact.Constants.SF_Owner_IdField];
                var entity = sfEntityUtility.GetEntityByEntityId(ownerId);

                if (entity != null)
                {
                    return entity.InternalFields.Contains(userKey) ? entity.InternalFields[userKey] : null;
                }
            }

            return null;
        }

        public static Owner GetOwner(S4SInfo info)
        {
            if (info.Fields.TryGetValue(Foundation.Contact.Constants.SF_Owner_EmailField, out var ownerEmail) &&
                info.Fields.TryGetValue(Foundation.Contact.Constants.SF_Owner_NameField, out var ownerName) &&
                !string.IsNullOrEmpty(ownerEmail) && !string.IsNullOrEmpty(ownerName))
            {
                return new Owner { Name = ownerName, Email = ownerEmail };
            }

            var sfEntityUtility = ServiceLocator.ServiceProvider.GetService<ISFEntityUtility>();

            if (sfEntityUtility.IsLead(info.Fields[Foundation.Contact.Constants.SF_IdField]))
            {
                var ownerId = info.Fields[Foundation.Contact.Constants.SF_Owner_IdField];
                var entity = sfEntityUtility.GetEntityByEntityId(ownerId);

                if (entity != null)
                {
                    ownerEmail = entity.InternalFields.Contains(Foundation.Contact.Constants.SF_User_EmailField) ? entity.InternalFields[Foundation.Contact.Constants.SF_User_EmailField] : null;
                    ownerName = entity.InternalFields.Contains(Foundation.Contact.Constants.SF_User_NameField) ? entity.InternalFields[Foundation.Contact.Constants.SF_User_NameField] : null;

                    if (!string.IsNullOrEmpty(ownerEmail) && !string.IsNullOrEmpty(ownerName))
                    {
                        return new Owner { Name = ownerName, Email = ownerEmail };
                    }
                }
            }

            return null;
        }
    }
}