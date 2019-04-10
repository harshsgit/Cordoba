using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CordobaModels.Entities
{
    public class ProductEntity
    {
        public int product_id { get; set; }
        public string name { get; set; }

        #region Data Tab

        public string Image { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public int? minimum { get; set; }
        public int? subtract { get; set; }
        public int? stock_status_id { get; set; }
        public int? shipping { get; set; }
        public System.DateTime? date_available { get; set; }
        public int? status { get; set; }
        public string StatusName { get; set; }
        public int? sort_order { get; set; }
        public double? points { get; set; }
        #endregion Data Tab

        #region Shipping Details

        public int? country_id { get; set; }
        public decimal shipping_cost { get; set; }
        public decimal? cost { get; set; }
        public string countryName { get; set; }

        #endregion Shipping Details

        #region Links 

        public int? manufacturer_id { get; set; }
        public int? category_id { get; set; }
        public int? supplier_id { get; set; }

        #endregion Links

        public DateTime? date_added { get; set; }
        public DateTime? date_modified { get; set; }
        public int TotalRecords { get; set; }
        public string CatalogueIdCSV { get; set; }
        public List<ProductDescriptionList> ProductDescriptionList { get; set; }
        public List<CatalogueEntity> CatalogueList { get; set; }
        public bool? IsSelected { get; set; }
        public bool? IsExcluded { get; set; }

        #region Extra Details

        public string description { get; set; }
        public int? CartQuantity { get; set; }
        public string SupplierName { get; set; }
        public string ManufactureName { get; set; }
        public string ManufactureImage { get; set; }
        public bool? IsProductInWishlist { get; set; }
        #endregion Extra Details

        //public dynamic data { get; set; }
        public string StoreIds { get; set; }
        public int? store_id { get; set; }

    }
}
