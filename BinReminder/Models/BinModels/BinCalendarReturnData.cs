using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.BinModels
{
    public class BinCalendarReturnData
    {
        [JsonProperty("results_returned")]
        public bool ResultsReturned { get; set; }
        [JsonProperty("tab_collections")]
        public IEnumerable<Bin> Bins { get; set; }
    }
}
