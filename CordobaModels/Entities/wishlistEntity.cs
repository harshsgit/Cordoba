using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class wishlistEntity
    {
        public int? wishid { get; set; }
        public int? customer_id { get; set; }
        public int? product_id { get; set; }
        public int? store_id { get; set; }       
        public int? createdby { get; set; }
        public Nullable<DateTime> createddate { get; set; }
        public int? updateby { get; set; }
        public Nullable<DateTime> updatedate { get; set; }
    }
}
