using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaProductImageDownloadService
{
    public class ProductCatalogue
    {
        public int product_id { get; set; }
        public string code { get; set; }
        public bool IsOperationCompleted { get; set; }
        public string image_full_url { get; set; }
        public string image_full { get; set; }
        public string CatalogueName { get; set; }
    }
}
