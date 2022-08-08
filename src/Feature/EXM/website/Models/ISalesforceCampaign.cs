﻿using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace LionTrust.Feature.EXM.Models
{
    public interface ISalesforceCampaign : IExmGlassBase
    {
        [SitecoreField(Constants.SalesforceCampaign.SalesforceCampaignId_FieldID)]
        string SalesforceCampaignId { get; set; }
    }
}