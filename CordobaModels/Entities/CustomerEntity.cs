using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class CustomerEntity
    {
        public int customer_id { get; set; }
        public int? store_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public int points { get; set; }
        public string password { get; set; }
        public string cart { get; set; }
        public int newsletter { get; set; }
        public int address_id { get; set; }
        public int status { get; set; }
        public int approved { get; set; }
        public Int16 activated { get; set; }
        public int customer_group_id { get; set; }
        public string customer_group_name { get; set; }
        public int? customer_department_id { get; set; }
        public string ip { get; set; }
        public DateTime? date_added { get; set; }

        public string WholeName { get; set; }
        public Int16 is_admin { get; set; }

        public string customerName { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedDate { get; set; }

        public int TotalRecords { get; set; }

        public int? cartgroup_id { get; set; }
        public int? TotalItemAdded { get; set; }

        public List<AddressEntity> AddressList { get; set; }

        public int? ErrorTypeId { get; set; }
        public List<PointsAuditEntity> PointsAuditList { get; set; }
        public List<TransactionListEntity> TransactionList { get; set; }


        public int? otp { get; set; }
        public string store_name { get; set; }
        public string store_url { get; set; }
        public string logo { get; set; }

        public string error { get; set; }
        public int errorcode { get; set; }
        public string Image { get; set; }
        public string dept_name { get; set; }
        public int? customerImage_Id { get; set; }
        public string localhosturl { get; set; }
        public bool IsFromAdmin { get; set; }
        public int? Language { get; set; }
        public bool? is_department { get; set; }
        public string JWTToken { get; set; }

        public string Comment { get; set; }
    }

}


