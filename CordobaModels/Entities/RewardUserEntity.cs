using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class RewardUserEntity
    {
        public int reward_user_id { get; set; }
        public int reward_id { get; set; }
        public int reward_giventouser_id { get; set; }
        public bool IsWinner { get; set; }
        public string Rewards { get; set; }
        public string Customer { get; set; }
        public string RewardedCustomer { get; set; }
        public string RewardedByCustomer { get; set; }
        public string Medal { get; set; }
        public int? id { get; set; }
        public string Customer_img { get; set; }
        public string Reward_Customer_Img { get; set; }
        public string comment { get; set; }
        public string Title { get; set; }
        public string RewardType { get; set; }
        public DateTime? winner_declared_date { get; set; }
        public string WinnerName { get; set; }
    }
}
