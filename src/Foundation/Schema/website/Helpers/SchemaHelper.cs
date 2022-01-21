using LionTrust.Foundation.Schema.Models;
using Schema.NET;
using System;
using System.Collections.Generic;

namespace LionTrust.Foundation.Schema.Helpers
{
    public class SchemaHelper
    {
        public static Organization GetOrganizationSchema(OrganizationSchema organizationSchema)
        {
            if (organizationSchema == null)
            {
                return null;
            }

            var logoObj = new ImageObject() 
            {
                Url = new Uri(organizationSchema.LogoUrl),
            };

            var geoCoordinates = new GeoCoordinates()
            {
                Longitude = organizationSchema.Longitude,
                Latitude = organizationSchema.Latitude
            };

            var address = new PostalAddress()
            {
                StreetAddress = organizationSchema.Address,
                AddressLocality = organizationSchema.Location,
                PostalCode = organizationSchema.PostalCode
            };

            var contactPoint = new ContactPoint()
            {
                Telephone = organizationSchema.Telephone,
                ContactType = organizationSchema.ContactType,
                AreaServed = organizationSchema.AreaServed
            };

            return new Organization
            {
                Name = organizationSchema.Name,
                Url = new Uri(organizationSchema.Url),
                Logo = logoObj,
                Description = organizationSchema.Description,
                SameAs = organizationSchema.SameAs, 
                ContactPoint = contactPoint,
                Email = organizationSchema.Email,
                Location = new Place() { Geo = geoCoordinates },
                Address = address
            };
        }
        
        public static BreadcrumbList GetBreadcrumbListSchema(BreadcrumbListSchema breadcrumbListSchema)
        {
            if (breadcrumbListSchema == null || breadcrumbListSchema.BreadcrumbItems == null)
            {
                return null;
            }

            var itemList = new List<ListItem>();
            foreach (var breadcrumbItem in breadcrumbListSchema.BreadcrumbItems)
            {
                var item = new WebPage()
                {
                    Id = new Uri(breadcrumbItem.Url ?? "about:blank"),
                    Name = breadcrumbItem.Name
                };
                itemList.Add(new ListItem()
                {
                    Position = breadcrumbItem.Position,
                    Item = item
                });
            }

            return new BreadcrumbList
            {
                ItemListElement = itemList
            };
        }

        public static Article GetArticleSchema(ArticleSchema articleSchema)
        {
            if (articleSchema == null)
            {
                return null;
            }

            var authorList = new List<Person>();
            if (articleSchema.Authors != null)
            {
                articleSchema.Authors.ForEach(a => authorList.Add(new Person() { Name = a })); 
            }

            var imageObj = new ImageObject()
            {
                Url = new Uri(articleSchema.ImageUrl ?? "about:blank"),
            };

            var logoObj = new ImageObject()
            {
                Url = new Uri(articleSchema.LogoUrl),
            };

            return new Article
            {
                Headline = articleSchema.Headline,
                MainEntityOfPage = new WebPage()
                {
                    Id = new Uri(articleSchema.Url)
                },
                Description = articleSchema.Description,
                DatePublished = new DateTimeOffset(articleSchema.DatePublished),
                DateModified = new DateTimeOffset(articleSchema.DateModified),
                Image = imageObj,
                Author = authorList,
                Publisher = new Organization()
                {
                    Name = articleSchema.PublisherName,
                    Logo = logoObj
                },
                ArticleBody = articleSchema.ArticleBody
            };
        }

    }
}