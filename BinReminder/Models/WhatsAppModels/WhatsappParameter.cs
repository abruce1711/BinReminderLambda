using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.WhatsAppModels
{
    public class WhatsAppParameter
    {
        [JsonProperty("type")]
        public string Type => "text";

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
