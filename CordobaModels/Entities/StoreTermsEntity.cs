using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class StoreTermsEntity
    {
        public int Store_Id { get; set; }
        public int Language_Id { get; set; }
        public string Terms { get; set; }
    }
}
