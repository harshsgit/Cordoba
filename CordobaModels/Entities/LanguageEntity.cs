using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class LanguageEntity
    {
        public int? language_id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string locale { get; set; }
        public string image { get; set; }
        public string directory { get; set; }
        public string filename { get; set; }
        public int sort_order { get; set; }
        public int status { get; set; }
    }
}
