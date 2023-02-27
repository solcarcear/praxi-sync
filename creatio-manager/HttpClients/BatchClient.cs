using creatio_manager.Model;
using creatio_manager.Model.BatchRequests;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using praxi_model;
using praxi_model.Creatio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace creatio_manager.HttpClients
{
 
    public class BatchClient
    {
        private readonly HttpClient _client;
        const string batchOdataUrl = "0/odata/$batch";
        public BatchClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<ContactDto>> ExeBatch()
        {
            var asdasd = new Contact();
            JsonContent content = JsonContent.Create<Contact>(asdasd);

            var msg = new HttpRequestMessage(HttpMethod.Post, batchOdataUrl);

            msg.Content = JsonContent.Create(new
            {
                requests= new List<BatchRequest>().ToArray(),
            });

            var response = await _client.SendAsync(msg);
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<CreatioResultDto<ContactDto>>(responseStr);


                return result?.value ?? new List<ContactDto>();
            }
            else
            {
                return new List<ContactDto>();
            }

        }
    }
}
