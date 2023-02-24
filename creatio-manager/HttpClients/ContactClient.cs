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
 
    public class ContactClient
    {
        private readonly HttpClient _client;
        const string contactOdataUrl = "0/odata/Contact";
        public ContactClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<ContactDto>> GetContactsToSync()
        {

            var query = new Dictionary<string, string>()
            {
                ["$filter"] = "UsrSincronizar eq true",
                ["$select"] = "Id,Name,Surname,Email,JobTitle,UsrSincronizar,ModifiedOn"
            };
            var uri = QueryHelpers.AddQueryString(contactOdataUrl, query);

            var msg = new HttpRequestMessage(HttpMethod.Get, uri);

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
