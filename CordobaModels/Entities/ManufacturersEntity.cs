using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
  public  class ManufacturersEntity
    {
      public long manufacturer_id { get; set; }
      public string name { get; set; }
      public int sort_order { get; set; }
      public string image { get;set;}
      public ManufacturersStoreEntity ManufacturerStoreList { get; set; }
      public string StoreIdCSV { get; set; }

      
    }
}
