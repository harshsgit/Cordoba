using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class ImportCustomerEmailEntity
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StoreName { get; set; }
        public string ErrorLog { get; set; }
        public string URL { get; set; }
    }
}
