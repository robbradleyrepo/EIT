﻿using Newtonsoft.Json;
using Sitecore.EmailCampaign.Server.Contexts;

namespace LionTrust.Feature.EXM.Models
{
    public class LTUpdateMessageContext : UpdateMessageContext
    {
        [JsonProperty("team")]
        public string Team { get; set; }
    }
}