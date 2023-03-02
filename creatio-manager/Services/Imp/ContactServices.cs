using praxi_model.Mupi;
using Newtonsoft.Json;
using creatio_manager.HttpClients;
using bussines_manager.Services;
using creatio_manager.Model.BatchRequests;
using praxi_model.Creatio;
using System.Net.Http.Json;
using creatio_manager.Model;

namespace creatio_manager.Services.Imp
{
    public class ContactServices : IContactServices
    {
        private readonly ContactClient _httpClient;
        private readonly BatchClient _batchClient;
        private readonly CreatioUtils _creatioUtils;

        private readonly IMupiContactService _mupiContactService;




        public ContactServices(ContactClient httpClientFactory,
                               IMupiContactService mupiContactService,
                               BatchClient batchClient,
                               CreatioUtils creatioUtils)
        {

            _httpClient = httpClientFactory;
            _mupiContactService = mupiContactService;
            _batchClient = batchClient;
            _creatioUtils = creatioUtils;
        }

        public async Task<ResultSyncDto> SyncContacts()
        {
            var result = new ResultSyncDto
            {
                Status = ResultSyncDto.resultOK,
                Message = "OK"
            };

            try
            {
                //GET ALL CONTACT THAT NEEDS SYNC FROM CREATIO
                var contactstoSync = await _httpClient.GetContactsToSync();

                //UPDATE CONTACT ON MUPI DB 

                var contactsSynced = _mupiContactService.SyncMupiContactsFromCreatio(contactstoSync.ToList());


                //UPDATE THE FLAG SYNC FOT EVERY CONTACT IN CREATIO
            }
            catch (Exception ex)
            {
                result.Trace = JsonConvert.SerializeObject(ex);
                result.Message = ex.Message;
                result.Status = ResultSyncDto.resultError;
            }

            return result;
        }       

        public async Task<ResultSyncDto> SyncContactsMupiToCreatio(DateTime from)
        {
            var result = new ResultSyncDto
            {
                Status = ResultSyncDto.resultOK,
                Message = "OK"
            };

            try
            {
                //GET ALL CONTACTS FROM MUPI TO SYNC
                var mupiContacts= _mupiContactService.GetContactsToSync(from);

                //SHAPE THE REQUESTS TO SEND TO CREATIO

                var batchRequests = await ShapeRequestsBatch(mupiContacts);

                //SEND REQUESTS TO CREATIO

                var responses =await _batchClient.RequestBatch(batchRequests);

                //MANAGE RESPONSES REQUESTS 



            }
            catch (Exception ex)
            {
                result.Trace = JsonConvert.SerializeObject(ex);
                result.Message = ex.Message;
                result.Status = ResultSyncDto.resultError;
            }

            return  result;
        }


        private async Task<IEnumerable<BatchRequest>> ShapeRequestsBatch(IEnumerable<ContactDto> contacts)
        {
            var result= new List<BatchRequest>();

            var count = 1;
            foreach (var contact in contacts)
            {

                var url = nameof(Contact);
                var httpVerb = BatchRequest.POST;
                if (!string.IsNullOrEmpty(contact.IdCreatio))
                {
                    url += $"/{contact.IdCreatio}";
                    httpVerb = BatchRequest.PATCH;
                }


                var creatioContact = new Contact
                {
                    UsrIDContacto = contact.Id.ToString(),
                    Name = contact.Name,
                    Surname = contact.Surname,
                    Email = contact.Email,
                    JobTitle = contact.JobTitle,
                    UsrSincronizar = false
                };


                var request = new BatchRequest
                {
                    Id = $"R{count}",
                    Method = httpVerb,
                    Url = url,
                    Headers = new BatchHeaders(),
                    Body = creatioContact
                };
                result.Add(request);
                count++;
                //        Body = _creatioUtils.ParseObjectToBodyRequest(creatioContact),                  

            }

            return result;

        }

    }
}
