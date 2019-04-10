using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class HotSpecialProductEntity
    {
        public int	hot_productid { get; set; }
        public int special_productid { get; set; }
        public int	store_id { get; set; }
        public int	product_id { get; set; }
        public int	priority { get; set; }
        public string productName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int	status { get; set; }
        public int	created_by { get; set; }
        public DateTime created_date { get; set; }
        public int	updated_by { get; set; }
        public DateTime updated_date { get; set; }
        public bool? IsHotProduct { get; set; }
        public int? PrimaryKeyId { get; set; }
        public string store_name { get; set; }
		
    }
}
