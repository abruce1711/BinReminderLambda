using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.BinModels
{
    public class Bin
    {
        [JsonProperty("colour")]
        public string Colour { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        public bool IsBeingCollectedTomorrow => Date == DateTime.Today.AddDays(1);
        public bool IsBeingCollectedTomorrowOrTheNextDay => Date <= DateTime.Today.AddDays(2);
    }
}
