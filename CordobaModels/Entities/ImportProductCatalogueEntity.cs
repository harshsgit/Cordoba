using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class ImportProductCatalogueEntity
    {
        public int product_id { get; set; }
        public string code { get; set; }
        public string brand { get; set; }
        public string name { get; set; }
        public decimal price_excl_vat { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string category_name { get; set; }
        public string image_full { get; set; }
        public int lastupdated { get; set; }
        public decimal delivery_zone_PIL_RATE_1 { get; set; }
        public decimal delivery_zone_PIL_RATE_2 { get; set; }
        public decimal delivery_zone_PIL_RATE_3 { get; set; }
        public decimal delivery_zone_PIL_RATE_4 { get; set; }
        public decimal delivery_zone_PIL_RATE_5 { get; set; }
        public decimal delivery_zone_PIL_RATE_6 { get; set; }
        public decimal delivery_zone_PIL_RATE_7 { get; set; }
        public decimal delivery_zone_PIL_RATE_8 { get; set; }
        public decimal delivery_zone_PIL_RATE_9 { get; set; }
        public decimal delivery_zone_PIL_RATE_10 { get; set; }
        public decimal delivery_zone_PIL_RATE_11 { get; set; }
        public decimal delivery_zone_PIL_RATE_12 { get; set; }
        public decimal delivery_zone_PIL_RATE_13 { get; set; }
        public string image_thumbnail_url { get; set; }
        public string image_preview_url { get; set; }
        public string image_full_url { get; set; }
        public string image_150_url { get; set; }
        public string image_300_url { get; set; }
    }
}
