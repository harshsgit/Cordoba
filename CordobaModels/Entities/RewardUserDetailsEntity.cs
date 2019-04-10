using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class RewardUserDetailsEntity
    {
        public int id { get; set; }
        public int reward_user_id { get; set; }
        public int reward_givenby_userid { get; set; }
        public int reward_value { get; set; }
        public DateTime reward_given_date { get; set; }
        public string comment { get; set; }
        public string Medal { get; set;}
        public string Customer { get; set; }
        public int reward_type_id { get; set; }
    }

    public class AddRewardEntity
    {
        public int customer_id { get; set; }
        public string customer { get; set; }
        public byte IsRewarded { get; set; }
        public string reward_name { get; set; }
        public string CustomerImage { get; set; }
        public int? reward_id { get; set; }
        public bool? IsWinner { get; set; }
        public int? loginUserid { get; set; }
        public string Comment { get; set; }
        public int? reward_type_id { get; set; }
        public int? NoOfStars { get; set; }
        public int? reward_value { get; set; }
    }
}
