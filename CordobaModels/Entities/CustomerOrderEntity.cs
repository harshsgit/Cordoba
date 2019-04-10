using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class CustomerOrderEntity
    {
       int? CustomerId { get; set; }
       public string CustomerName { get; set; }
       public string Email { get; set; }
       public string CustomerGroupName { get; set; }
       public string StatusName { get; set; }
       public int? NoOfOrders { get; set; }
       public int? NoOfProducts { get; set; }
       public int? Total { get; set; }
    }
}
