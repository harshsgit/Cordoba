using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class CountryEntity
    {
        public int? country_id { get; set; }
        public string iso_code_2 { get; set; }
        public string iso_code_3 { get; set; }
        public string address_format { get; set; }
        public int postcode_required { get; set; }
        public string name { get; set; }
        public int? Status { get; set; }
        public decimal? shipping_cost { get; set; }
    }
}
