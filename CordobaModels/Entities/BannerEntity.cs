using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class BannerEntity
    {

        public int? banner_id { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public string StatusName { get; set; }
        public List<BannerAttributeEntity> BannerAttributeList {get;set;}
        public string link { get; set; }
        public string image { get; set; }
        public int sort_order { get; set; }
        public int? banner_image_id { get; set;}
    }
}
