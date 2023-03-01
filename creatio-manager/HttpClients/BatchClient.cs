using Azure.Core;
using creatio_manager.Model.BatchRequests;
using creatio_manager.Services.Imp;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace creatio_manager.HttpClients
{
 
    public class BatchClient
    {
        private readonly HttpClient _client;
        private readonly CreatioUtils _creatioUtils;

        const string batchOdataUrl = "0/odata/$batch";
        public BatchClient(HttpClient client, CreatioUtils creatioUtils)
        {
            _client = client;
            _creatioUtils = creatioUtils;
        }


        public async Task<BatchResult> RequestBatch(IEnumerable<BatchRequest> requests)
        { 
            var result = new BatchResult();
            for (int i = 0; i < requests.Count(); i= i+1)
            {
                var batchRequest = requests.Skip(i).Take(1);

                var batchResults = await ExecuteBatch(batchRequest);

                result.Responses.AddRange(batchResults.Responses);

            }


            return result;
        
        }


        private async Task<BatchResult> ExecuteBatch(IEnumerable<BatchRequest> requests)
        {
            var result = new BatchResult();

            var msg = new HttpRequestMessage(HttpMethod.Post, batchOdataUrl);

            var bodyContent = _creatioUtils.ParseObjectToBodyRequest(new
            {
                requests = requests.Select(x =>

                    new
                    {
                        x.Id,
                        x.Method,
                        x.Url,
                        Body = JsonConvert.DeserializeObject(x.Body.Value.ToString()),
                        Headers = JsonConvert.DeserializeObject(x.Headers.Value.ToString())
                    }


                ).ToArray(),
            });

            msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            msg.Content = new StringContent(JsonConvert.SerializeObject(bodyContent), Encoding.UTF8);
            msg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");




            var response = await _client.SendAsync(msg);

          
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<BatchResult>(responseStr);
            }

            return result;


        }
    }
}
