﻿using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data;

namespace LionTrust.Feature.LeadBanner.Models
{
    public interface ILookupValue
    {
        ID Id { get; set; }

        [SitecoreField("LookupValue_Value")]
        string Value { get; set; }
    }
}