using System.Net.Http.Json;

namespace creatio_manager.Model.BatchRequests
{
    public class BatchRequest
    {
        static readonly string GET = "GET";
        static readonly string POST = "POST";
        static readonly string PATCH = "PATCH";
        static readonly string PUT = "PUT";
        static readonly string DELETE = "DELETE";
        public string Method { get; set; }
        public string Url { get; set; }
        public JsonContent Body { get; set; }
        public JsonContent Headers { get; set; }
    }
}
