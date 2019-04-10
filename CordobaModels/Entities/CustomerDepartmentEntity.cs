using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class CustomerDepartmentEntity
    {
        public int customer_department_id { get; set; }
        public int store_id { get; set; }
        public string store_name { get; set; }
        public int parent_id { get; set; }
        public string name { get; set; }
        public int LoggedInUserId { get; set; }
        public bool status { get; set; }
        public string showstatus { get; set; }
    }
}
