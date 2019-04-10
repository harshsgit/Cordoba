using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface IRewardService
    {
        List<RewardEntity> GetRewardList(int StoreId, int LoggedInUserId, int reward_id = 0);
        List<RewardTypeEntity> GetRewardTypeList(int StoreId, int LoggedInUserId);
        int InsertOrUpdateReward(int StoreId, int LoggedInUserId, RewardEntity objRewardEntity, int isAddMode);
        int DeleteReward(int StoreId, int LoggedInUserId, int reward_id);
        List<RewardUserEntity> ViewCustomerRewards(int StoreId, int LoggedInUserId, int reward_id);
        List<RewardUserDetailsEntity> GetRewardCustomerDetails(int StoreId, int LoggedInUserId, int reward_user_id);
        int DeleteRewardUser(int StoreId, int LoggedInUserId, int id, int reward_user_id);
        List<RewardUserDetailsEntity> MyRewards(int StoreId,int id);
        List<RewardEntity> GetAllRunningRewards(int StoreId, int LoggedInUserId);
        List<RewardUserEntity> RewardCustomerDetails(int StoreId, int LoggedInUserId,int reward_id);
        List<AddRewardEntity> GetRewardGroupCustomers(int StoreId, int loginUserId,int reward_id);
        int AddCustomer_Reward(int StoreId, int LoggedInUserId,AddRewardEntity objAddRewardEntity);
        int Declare_RewardWinner(int StoreId, int LoggedInUserId, int reward_id, int reward_user_id, string admin_comment);
        int Delete_RewardWinner(int StoreId, int LoggedInUserId, int reward_user_id);
        List<RewardUserEntity> Dashboard_RewardWinner(int StoreId, int LoggedInUserId);
    }
}
