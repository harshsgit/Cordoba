using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class CartEntity
    {
        public int? cart_id { get; set; }
        public int? store_id { get; set; }
        public int? customer_id { get; set; }
        public int? product_id { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public int? quantity { get; set; }
        public int? cartgroup_id { get; set; }
        public decimal? price { get; set; }
        public double? points { get; set; }
        public double? totalpoints { get; set; }
        public decimal? total { get; set; }
        public decimal? tax { get; set; }
        public Nullable<DateTime> createddate { get; set; }
        public int? createdby { get; set; }
        public Nullable<DateTime> updateddate { get; set; }
        public int? updatedby { get; set; }

        public int? TotalItemAdded { get; set; }

        public int? ProductAvailableQuantity { get; set; }
        public bool? IsOutOfStock { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? OrderTotal { get; set; }
        public string ProductImage { get; set; }

        public decimal? AllItemSubtotal { get; set; }
        public decimal? AllItemTotal { get; set; }
        public decimal? CustomerAvailablePoints { get; set; }

        public double? AllItemSubtotalPoints { get; set; }
        public double? AllItemTotalPoints { get; set; }

        public bool? IsProductInWishlist { get; set; }

        public int? categoryStatus { get; set; }
        public int? parentCategorystatus { get; set; }
        public int? productStatus { get; set; }
        public int? productQty { get; set; }

    }
}
