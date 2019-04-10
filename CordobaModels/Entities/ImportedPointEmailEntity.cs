using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class ImportedPointEmailEntity
    {
        public string CustomerName { get; set; }
        public string Email{ get; set; }
        public int TotalPoints { get; set; }
        public int ImportedPoints { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string URL { get; set; }
    }
}
