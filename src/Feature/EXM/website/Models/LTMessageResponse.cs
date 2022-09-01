using Newtonsoft.Json;
using Sitecore.EmailCampaign.Server.Responses;

namespace LionTrust.Feature.EXM.Models
{
    public class LTMessageResponse : MessageResponse
    {
        [JsonProperty(Constants.ExmMessage.Message)]
        public new LTMessage Message { get; set; }

        public LTMessageResponse(MessageResponse message)
        {
            Message = new LTMessage(message.Message);
            NotFound = message.NotFound;
            ItemNameValidation = message.ItemNameValidation;
        }
    }
}