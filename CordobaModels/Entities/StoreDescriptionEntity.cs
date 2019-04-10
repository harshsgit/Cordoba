using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class StoreDescriptionEntity 
    {
       public int? store_id { get; set; }
       public int? language_id { get; set; }
       public string description { get; set; }
    }
}
