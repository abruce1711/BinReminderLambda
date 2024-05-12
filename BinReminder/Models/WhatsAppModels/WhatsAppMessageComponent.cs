using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.WhatsAppModels
{
    public class WhatsAppMessageComponent
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("parameters")]
        public List<WhatsAppParameter> Parameters { get; set; } = new();
    }
}
