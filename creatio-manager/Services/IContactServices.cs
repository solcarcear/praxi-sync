using creatio_manager.Model;
using praxi_model.Mupi;


namespace creatio_manager.Services
{
    public interface IContactServices
    {
        public  Task<ResultSyncDto> SyncContacts();


    }
}
