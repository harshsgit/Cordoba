using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class CategoryStoreEntity
    {

        public int? CategoryId { get; set; }
        public List<StoreEntity> CategoryStore { get; set; }

    }
}
