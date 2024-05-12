using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.WhatsAppModels
{
    public class WhatsAppMessageTemplate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("components")]
        public List<WhatsAppMessageComponent> Components { get; set; } = new();

        [JsonProperty("language")]
        public WhatsAppMessageLanguage Language = new();

        public WhatsAppMessageTemplate(string name)
        {
            Name = name;
        }
    }
}
