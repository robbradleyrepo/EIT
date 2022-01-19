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
                Height = organizationSchema.LogoHeight,
                Width = organizationSchema.LogoWidth
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

        public static Article GetArticleSchema()
        {
            return new Article
            {
                
            };
        }

        public static BreadcrumbList GetBreadcrumbListSchema()
        {
            return new BreadcrumbList
            {

            };
        }
    }
}