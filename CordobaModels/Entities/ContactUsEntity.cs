using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class ContactUsEntity
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public Int64 phone { get; set; }
        public string description { get; set; }
    }
}
