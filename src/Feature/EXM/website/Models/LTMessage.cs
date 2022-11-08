using Newtonsoft.Json;
using Sitecore.EmailCampaign.Server.Model;

namespace LionTrust.Feature.EXM.Models
{
    public class LTMessage : Message
    {
        [JsonProperty(Constants.ExmMessage.Team)]
        public string Team { get; set; }

        [JsonProperty(Constants.ExmMessage.TeamPath)]
        public string TeamPath { get; set; }

        [JsonProperty(Constants.ExmMessage.KeepDefaultSender)]
        public bool KeepDefaultSender { get; set; }

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