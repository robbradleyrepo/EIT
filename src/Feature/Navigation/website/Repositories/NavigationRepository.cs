namespace LionTrust.Feature.Navigation.Repositories
{
    using System;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Navigation.Models;
    using LionTrust.Foundation.Schema.Models;
    using LionTrust.Foundation.SitecoreExtensions.Extensions;
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;

    public class NavigationRepository : INavigationRepository
    {
        public Item ContextItem { get; }
        public Item NavigationRoot { get; }

        public NavigationRepository(Item contextItem)
        {
            this.ContextItem = contextItem;
            this.NavigationRoot = this.GetNavigationRoot(this.ContextItem);
            if (this.NavigationRoot == null)
            {
                throw new InvalidOperationException($"Cannot determine navigation root from '{this.ContextItem.Paths.FullPath}'");
            }
        }

        public Item GetNavigationRoot(Item contextItem)
        {
            return contextItem.GetAncestorOrSelfOfTemplate(new ID(Navigation.Constants.NavigationRoot.TemplateID)) ?? Context.Site.GetContextItem(new ID(Navigation.Constants.NavigationRoot.TemplateID));
        }

        public OrganizationSchema GetOrganizationData(IHome home, IMvcContext mvcContext)
        {
            var organizationSchema = new OrganizationSchema
            {
                Url = home.AbsoluteUrl,
                Description = home.PageShortDescription,
                Name = home.CompanyName,
                ContactType = home.ContactType,
                AreaServed = home.AreaServed
            };

            if (home.SchemaLogo != null)
            {
                var imageItem = mvcContext.SitecoreService.GetItem<Item>(home.SchemaLogo.MediaId);
                if (imageItem != null)
                {
                    var mediaOption = new MediaUrlOptions()
                    {
                        AlwaysIncludeServerUrl = true,
                        AbsolutePath = true,
                        LowercaseUrls = true,
                        RequestExtension = ""
                    };
                    organizationSchema.LogoUrl = MediaManager.GetMediaUrl(imageItem, mediaOption);
                }
            }
            else
            {
                if (home.Logo != null)
                {
                    var imageItem = mvcContext.SitecoreService.GetItem<Item>(home.Logo.MediaId);
                    if (imageItem != null)
                    {
                        var mediaOption = new MediaUrlOptions()
                        {
                            AlwaysIncludeServerUrl = true,
                            AbsolutePath = true,
                            LowercaseUrls = true,
                            RequestExtension = ""
                        };
                        organizationSchema.LogoUrl = MediaManager.GetMediaUrl(imageItem, mediaOption);
                    }
                }
            }

            var footerConfig = home.FooterConfiguration;
            if (footerConfig != null)
            {
                organizationSchema.Telephone = footerConfig.PhoneNumber;
                organizationSchema.Email = footerConfig.Email;
                organizationSchema.Address = footerConfig.Address;
                organizationSchema.Location = footerConfig.Location;
                organizationSchema.PostalCode = footerConfig.PostalCode;
                organizationSchema.Longitude = footerConfig.Longitude;
                organizationSchema.Latitude = footerConfig.Latitude;

                if (footerConfig.SameAs != null)
                {
                    var linkList = new List<Uri>();
                    foreach (var socialLink in footerConfig.SameAs)
                    {
                        if (socialLink.SocialLink != null && !string.IsNullOrEmpty(socialLink.SocialLink.Url))
                        {
                            linkList.Add(new Uri(socialLink.SocialLink.Url));
                        }
                    }

                    organizationSchema.SameAs = linkList;
                }
            }

            return organizationSchema;
        }        
    }
}