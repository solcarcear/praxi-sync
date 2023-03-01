
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace creatio_manager.Services.Imp
{
    public class CreatioUtils
    {
        public JsonContent ParseObjectToBodyRequest<T>(T body )
        {

            return JsonContent.Create(JsonConvert.SerializeObject(body,
                            Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }
                    ));

        }

    }
}
