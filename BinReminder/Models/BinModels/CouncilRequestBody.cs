using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.BinModels
{
    public class CouncilRequestBody
    {
        [JsonProperty("name")]
        public static string Name => "bin_calendar";
    }
}
