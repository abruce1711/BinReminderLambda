using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.BinModels
{
    public class BinCalendarRequestBody : CouncilRequestBody
    {

        [JsonProperty("data")]
        public static BinCalendarRequestData Data { get; set; }

        public BinCalendarRequestBody(string uprn)
        {
            Data = new BinCalendarRequestData(uprn);
        }
    }
}
