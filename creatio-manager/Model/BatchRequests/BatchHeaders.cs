using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creatio_manager.Model.BatchRequests
{
    public class BatchHeaders
    {
        public BatchHeaders() {

            ContentType = "application/json;odata=verbose";
            Accept = "application/json;odata=verbose";
            Prefer = "continue-on-error";
        }

        [JsonProperty(PropertyName = "Content-Type")]
        public string ContentType { get; set; }
        [JsonProperty(PropertyName = "Accept")]
        public string Accept { get; set; }
        [JsonProperty(PropertyName = "Prefer")]
        public string Prefer { get; set; }
  
    }
}
