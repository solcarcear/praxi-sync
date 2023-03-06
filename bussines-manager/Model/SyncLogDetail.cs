using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bussines_manager.Model
{
    [Table("SyncLogDetail")]
    public class SyncLogDetail
    {
        public long Id { get; set; }
        public string Retries { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public int StatusId { get; set; }
        public int SyncLogId { get; set; }




        public SyncLog SyncLog { get; set; }
        public StateManager Status { get; set; }

    }
}
