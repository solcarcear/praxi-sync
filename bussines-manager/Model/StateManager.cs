using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bussines_manager.Model
{
    [Table("StateManager")]
    public class StateManager
    {

        public int Id { get; set; }
        public string Entity { get; set; }
        public string StateValue { get; set; }


        public ICollection<SyncLog> SyncLogs { get; set; }  
        public ICollection<SyncLogDetail> SyncLogDetails { get; set; }
    }
}
