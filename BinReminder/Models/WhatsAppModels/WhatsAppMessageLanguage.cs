using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.WhatsAppModels
{
    public class WhatsAppMessageLanguage
    {
        [JsonProperty("code")]
        public const string Code = "en_GB";
    }
}
