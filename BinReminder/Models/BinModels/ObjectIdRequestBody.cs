using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Models.BinModels
{
    public class ObjectIdRequestBody : CouncilRequestBody
    {
        [JsonProperty("data")]
        public static ObjectIdRequestData Data { get; set; }

        public ObjectIdRequestBody(string postCode)
        {
            Data = new ObjectIdRequestData(postCode);
        }
    }
}
