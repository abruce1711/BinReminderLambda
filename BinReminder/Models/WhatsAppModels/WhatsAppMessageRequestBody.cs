using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.WhatsAppModels
{
    public class WhatsAppMessageRequestBody
    {
        [JsonProperty("messaging_product")]
        public const string MessagingProduct = "whatsapp";

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } = "template";

        [JsonProperty("template")]
        public WhatsAppMessageTemplate Template { get; set; } = new(AppConfig.DEFAULT_TEMPLATE);

    }
}
