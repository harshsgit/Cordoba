using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class UserEntity
    {
        public int? user_id { get; set; }
        public string username { get; set; }
        public int? user_group_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int status { get; set; }
        public string StatusName { get; set; }
        public string ip { get; set; }
        public string image { get; set; }
        public Nullable<DateTime> date_added { get; set; }
        public int? store_id { get; set; }
        public int IsStoreAdmin { get; set; }
        public int Is_ImportPoint { get; set; }
        public int? customer_department_id { get; set; }
        public int? is_department { get; set; }

    }
}
