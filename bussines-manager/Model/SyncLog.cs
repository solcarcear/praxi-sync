using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bussines_manager.Model
{
    [Table("SyncLog")]
    public class SyncLog
    {

        public long Id { get; set; }
        public string Module { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime FinishAt { get; set; }
        public int StatusId { get; set; }
        public string Notes { get; set; }


        public StateManager Status { get; set; }
        public ICollection<SyncLogDetail> SyncLogDetails { get; set; }

    }
}
