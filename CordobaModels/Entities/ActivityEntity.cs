using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class ActivityEntity
    {
        public Int64 activity_id { get; set; }
        public int entitykey { get; set; }
        public string activity_text { get; set; }
        public string activity_value { get; set; }
        public string comments { get; set; }
        public int activity_type { get; set; }
        public DateTime date_added { get; set; }
        public int createdby { get; set; }
        public string status { get; set; }
    }
}
