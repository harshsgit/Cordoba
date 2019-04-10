using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class TransactionListEntity
    {
        public int customer_id { get; set; }
        public string comment { get; set; }
        public Decimal total { get; set; }
        public DateTime date_added { get; set; }

    }
}
