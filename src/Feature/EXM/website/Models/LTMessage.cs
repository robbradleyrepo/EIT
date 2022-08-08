using Newtonsoft.Json;
using Sitecore.EmailCampaign.Server.Model;

namespace LionTrust.Feature.EXM.Models
{
    public class LTMessage : Message
    {
        [JsonProperty("team")]
        public string Team { get; set; }

        [JsonProperty("teamPath")]
        public string TeamPath { get; set; }

        public LTMessage(Message message)
        {
            UsesPersonalisation = message.UsesPersonalisation;
            StartTime = message.StartTime;
            LastTestEmail = message.LastTestEmail;
            IsAbTesting = message.IsAbTesting;
            Variants = message.Variants;
            MessageType = message.MessageType;
            SendingState = message.SendingState;
            MessageState = message.MessageState;
            Readonly = message.Readonly;
            HasCampaign = message.HasCampaign;
            UsePreferredLanguage = message.UsePreferredLanguage;
            Languages = message.Languages;
            Template = message.Template;
            Sender = message.Sender;
            TargetDevice = message.TargetDevice;
            CampaignGroupPath = message.CampaignGroupPath;
            CampaignGroup = message.CampaignGroup;
            CampaignCategoryPath = message.CampaignCategoryPath;
            CampaignCategory = message.CampaignCategory;
            Description = message.Description;
            Name = message.Name;
            Id = message.Id;
            Attachments = message.Attachments;
            IsServiceMessage = message.IsServiceMessage;
        }
    }
}