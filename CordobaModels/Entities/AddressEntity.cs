using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class AddressEntity
    {
        public int address_id { get; set; }
        public int customer_id { get; set; }
        public string company { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string postcode { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public int? country_id { get; set; }
        public string address { get; set; }
        public int? zone_id { get; set; }
        public bool? IsDefaultAddress { get; set; }
        public string telephone { get; set; }

    }
}
