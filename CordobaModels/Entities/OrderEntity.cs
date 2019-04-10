using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class OrderEntity
    {
        public int store_id { get; set; }
        public int order_id { get; set; }
        public string store_name { get; set; }
        public string store_url { get; set; }
        public string customer { get; set; }
        public int? customer_group_id { get; set; }
        public string CustomerGroupName { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public decimal? total { get; set; }
        public string OrderStatusName { get; set; }
        public string ip { get; set; }
        public string LanguageName { get; set; }
        public DateTime? date_added { get; set; }
        public DateTime? date_modified { get; set; }
        public string payment_firstname { get; set; }
        public string payment_lastname { get; set; }
        public string payment_address_1 { get; set; }
        public string payment_address_2 { get; set; }
        public string payment_city { get; set; }
        public string payment_country { get; set; }
        public int? payment_country_id { get; set; }
        public string payment_method { get; set; }
        public string payment_company { get; set; }
        public string shipping_address_1 { get; set; }
        public string shipping_address_2 { get; set; }
        public string shipping_city { get; set; }
        public string shipping_firstname { get; set; }
        public string shipping_lastname { get; set; }
        public string shipping_company { get; set; }
        public string shipping_country { get; set; }
        public int? shipping_country_id { get; set; }
        public string shipping_method { get; set; }
        public string shipping_postcode { get; set; }
        public string payment_postcode { get; set; }
        public string fax { get; set; }
        public string comment { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int? order_status_id { get; set; }
        public int? currency_id { get; set; }
        public string currencyTitle { get; set; }
        public int? payment_zone_id { get; set; }
        public int? customer_id { get; set; }
        public int? address_id { get; set; }
        public string payment_zone { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }

        public int TotalRecords { get; set; }

        
        public int TotalQtyInOrder { get; set; }
        public string OrderTotalText { get; set; }

        public double? totalpoints { get; set; }
        
        public List<OrderProductEntity> orderProductEntity { get; set; }

        public List<OrderHistoryEntity> orderHistoryEntity { get; set; }
    }
}