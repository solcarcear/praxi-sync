using System.ComponentModel.DataAnnotations.Schema;

namespace bussines_manager.Model
{
    [Table("sync_creatio_logs")]
    public class SyncCreatioLogs
    {
        public int Id { get; set; }

        public string ActionUser { get; set; }
        public string MessageLog { get; set; }
        public string ModuleId { get; set; }
        public string Module { get; set; }
        public string Result { get; set; }
        public string Notes { get; set; }
        public string Task { get; set; }
        public DateTime? ModifyOn { get; set; }

    }
}
