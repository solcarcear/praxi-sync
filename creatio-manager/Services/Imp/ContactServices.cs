using praxi_model.Mupi;
using Newtonsoft.Json;
using creatio_manager.HttpClients;
using bussines_manager.Services;


namespace creatio_manager.Services.Imp
{
    public class ContactServices : IContactServices
    {
        private readonly ContactClient _httpClient;
        private readonly IMupiContactService _mupiContactService;



        public ContactServices(ContactClient httpClientFactory, IMupiContactService mupiContactService)
        {

            _httpClient = httpClientFactory;
            _mupiContactService = mupiContactService;

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

                _mupiContactService.SyncMupiContactsFromCreatio(contactstoSync.ToList());


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

    }
}
