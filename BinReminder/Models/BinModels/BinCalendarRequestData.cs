using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.BinModels
{
    public class BinCalendarRequestData
    {
        [JsonProperty("uprn")]
        public string Uprn { get; set; }

        public BinCalendarRequestData(string uprn)
        {
            Uprn = uprn;
        }
    }
}
