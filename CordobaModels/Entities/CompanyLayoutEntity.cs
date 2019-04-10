using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
  public  class CompanyLayoutEntity
    {

      public int? CompanyLayoutId { get; set; }
      public string HostName { get; set; }
      public string LayoutName { get; set; }
      public string Description { get; set; }
      public int? CreatedBy { get; set; }
      public Nullable<DateTime> CreatedDate { get; set; }
    }
}
