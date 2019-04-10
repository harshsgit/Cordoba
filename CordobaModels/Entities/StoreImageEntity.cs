using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class StoreImageEntity
    {
        public int? Store_Id { get; set; }
        public string Image { get; set; }
        public int? ImageKey { get; set; }
        public int layout { get; set; }
    }
}
