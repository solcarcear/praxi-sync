using praxi_model.Creatio;
using praxi_model.Mupi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bussines_manager.Services
{
    public interface IMupiContactService
    {
        public ResultSyncDto SyncMupiContactsFromCreatio(List<ContactDto> ContactsCreatio);
    }
}
