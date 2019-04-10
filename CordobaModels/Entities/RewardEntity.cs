using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class RewardEntity
    {
        public int id { get; set; }
        public string Title { get; set; }
        public int reward_type_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int status { get; set; }
        public string description { get; set; }
        public bool sent_mail { get; set; }
        public bool display_dashborad { get; set; }
        public string RewardTypeName { get; set; }
        public int created_by { get; set; }
        public int? modified_by { get; set; }
        public int? Participate { get; set; }
        public int? store_id { get; set; }
        public bool IsCustomerDepartment { get; set; }
        public string store_name { get; set; }
    }

}
