using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class ZoneEntity
    {
        public int zone_id { get; set; }
        public int country_id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public Int16 status { get; set; }
    }
}
