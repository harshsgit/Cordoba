using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class CategoryPopularEntity
    {
        public int? category_popularId { get; set; }
        public int? category_Id { get; set; }
        public int? store_Id { get; set; }
        public DateTime? createddate { get; set; }
        public int? createdby { get; set; }
        public string name { get; set; }
        public bool? IsPopular { get; set; }
        public string image { get; set; }
    }
}
