using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class PlaceOrderEntity
    {
        public int? store_id { get; set; }
        public int? customer_id { get; set; }
        public int? shipping_addressId { get; set; }
        public string IpAddress { get; set; }
        public string Comment { get; set; }
        public int? CartGroupId { get; set; }

        // Order Detail for Email
        public int order_id { get; set; }
        public int invoice_id { get; set; }
        public string store_name { get; set; }
        public string store_logo { get; set; }
        public string customer_name { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string product_name { get; set; }
        public int quantity { get; set; }
        public decimal tax { get; set; }
        public decimal product_price { get; set; }
        public decimal total { get; set; }
        public string OrderStatusName { get; set; }
        public string payment_name { get; set; } //order status name
        public string payment_address { get; set; }
        public string payment_method { get; set; }
        //public string payment_company { get; set; }
        public string shipping_address { get; set; }
        public string shipping_name { get; set; }
        public string shipping_company { get; set; }
        public string shipping_method { get; set; }
        public string currencyTitle { get; set; }
        public DateTime OrderDate { get; set; }

        public string model { get; set; }

    }
}
