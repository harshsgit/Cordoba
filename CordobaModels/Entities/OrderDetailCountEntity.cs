using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class OrderDetailCountEntity
    {
        public string OrderStatusName { get; set; }
        public int OrderCount { get; set; }
        public string Name { get; set; }
        public int? NumberOfCount { get; set; }
        public int? type { get; set; }
        public int? Active { get; set; }
        public int? total { get; set; }
        public int? IncludeInTotal { get; set; }
    }
}
