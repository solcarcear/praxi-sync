using System.Net.Http.Json;

namespace creatio_manager.Model.BatchRequests
{
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
        public JsonContent Body { get; set; }
        public JsonContent Headers { get; set; }
    }
}
