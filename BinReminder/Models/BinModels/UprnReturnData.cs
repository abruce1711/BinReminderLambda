using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.BinModels
{
    public class UprnReturnData
    {
        [JsonProperty("property-UPRN")]
        public string propertyUprn { get; set; }
    }
}
