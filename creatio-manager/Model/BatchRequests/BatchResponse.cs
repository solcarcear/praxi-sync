using System.Net.Http.Json;

namespace creatio_manager.Model.BatchRequests
{
    public class BatchResponse
    {
        static readonly int STATUS_OK_CREATE = 201;
        static readonly int STATUS_OK_UPDATE_DELETE = 204;

        public string Id { get; set; }
        public int Status { get; set; }
        public JsonContent Body { get; set; }

    }
}
