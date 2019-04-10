using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class OrderHistoryEntity
    {
        public Int64 order_history_id { get; set; }
        public DateTime date_added { get; set; }
        public string comment { get; set; }
        public string name { get; set; }
        public int notify { get; set; }
        public Int64? order_id { get; set; }
        public int? order_status_id { get; set; }
    }
}
