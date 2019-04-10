using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class CustomerImageEntity
    {
        public int customerImage_Id { get; set; }
        public int customer_id { get; set; }
        public string Image { get; set; }
    }
}
