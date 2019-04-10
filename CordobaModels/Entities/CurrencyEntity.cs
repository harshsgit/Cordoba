using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class CurrencyEntity
    {
        public int currency_id { get; set; }
        public string title { get; set; }
        public string code { get; set; }
        public string symbol_left { get; set; }
        public string symbol_right { get; set; }
        public string decimal_place { get; set; }
        public double value { get; set; }
        public int status { get; set; }
        public DateTime date_modified { get; set; }
    }
}
