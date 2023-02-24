using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praxi_model.Mupi
{


    public class ResultSyncDto
    {
        public static readonly string resultOK = "Completed";
        public static readonly string resultError = "Error Sync";
        public string Status { get; set; }
        public string Message { get; set; }
        public string Trace { get; set; }
        public string Task { get; set; }
        public string Module { get; set; }

    }
}
