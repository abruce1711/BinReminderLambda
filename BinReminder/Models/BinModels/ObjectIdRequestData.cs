using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.BinModels
{
    public class ObjectIdRequestData
    {
        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        public ObjectIdRequestData(string postcode)
        {
            Postcode = postcode;
        }
    }
}
