
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace creatio_manager.Model.BatchRequests
{
    public class BatchRequests
    {
        public IEnumerable<BatchRequest> Requests{get;set;}

    }

    public class BatchRequest
    {
        public static readonly string GET = "GET";
        public static readonly string POST = "POST";
        public static readonly string PATCH = "PATCH";
        public static readonly string PUT = "PUT";
        public static readonly string DELETE = "DELETE";
        public string Id { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public object Body { get; set; }
        public BatchHeaders Headers { get; set; }
    }

    public class BatchHeaders
    {
        public BatchHeaders()
        {

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
