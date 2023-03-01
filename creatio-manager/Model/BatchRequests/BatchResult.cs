using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creatio_manager.Model.BatchRequests
{
    public class BatchResult
    {
        public BatchResult() {
            Responses= new BatchResponse[] { };
        }
        public BatchResponse[] Responses { get; set; }
    }
}
