using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace CordobaModels.Entities
{
    public class StoreEntity
    {
        public int store_id { get; set; }
        public string name { get; set; }
        public bool? IsSelected { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string meta_title { get; set; }
        public string meta_description { get; set; }
        public string template { get; set; }
        public int? country_id { get; set; }
        public string language { get; set; }
        public string currency { get; set; }
        public int? tax { get; set; }
        public int? customer_group_id { get; set; }
        public int? customer_price { get; set; }
        public int? customer_approval { get; set; }
        public int? allow_registration { get; set; }
        public int? require_login { get; set; }
        public int? multilingual { get; set; }
        public int? account_id { get; set; }
        public int? checkout_id { get; set; }
        public int? stock_display { get; set; }
        public int? stock_check { get; set; }
        public int? stock_checkout { get; set; }
        public int? order_status_id { get; set; }
        public string logo { get; set; }
        public string icon { get; set; }
        public int? catalog_limit { get; set; }
        [AllowHtml]
        public string css_overrides { get; set; }
        [AllowHtml]
        public string terms { get; set; }
        [AllowHtml]
        public string description { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string county { get; set; }
        public string layout { get; set; }
        public int? banner_id { get; set; }
        public string catalougeIdCsv { get; set; }
        public string no_image_path { get; set; }
        public bool? Is_ImportPoint { get; set; }
        public bool? Is_AccessStore { get; set; }
        
    }
}
