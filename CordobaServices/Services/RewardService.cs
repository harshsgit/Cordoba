using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class RewardService : IRewardService
    {
        private GenericRepository<RewardEntity> objGenericRepository = new GenericRepository<RewardEntity>();

        public List<RewardEntity> GetRewardList(int StoreId, int LoggedInUserId, int reward_id = 0)
        {
            List<RewardEntity> listOfRewards = new List<RewardEntity>();
            var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramRewardId = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = reward_id };
            listOfRewards = objGenericRepository.ExecuteSQL<RewardEntity>("GetRewardList", ParameterStoreId, ParameterLoggedInUserId, paramRewardId).ToList();
            return listOfRewards;
        }

        public List<RewardTypeEntity> GetRewardTypeList(int StoreId, int LoggedInUserId)
        {
            List<RewardTypeEntity> listOfRewardtype = new List<RewardTypeEntity>();
            var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            listOfRewardtype = objGenericRepository.ExecuteSQL<RewardTypeEntity>("GetRewardTypeList", ParameterStoreId, ParameterLoggedInUserId).ToList();
            return listOfRewardtype;
        }

        public int InsertOrUpdateReward(int StoreId, int LoggedInUserId, RewardEntity objRewardEntity, int isAddMode)
        {
            var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = objRewardEntity.store_id
                };

            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };

            var paramRewardId = new SqlParameter { ParameterName = "id", DbType = DbType.Int32, Value = objRewardEntity.id };
            var paramRewardTitle = new SqlParameter { ParameterName = "Title", DbType = DbType.String, Value = objRewardEntity.Title };
            var paramreward_type_id = new SqlParameter { ParameterName = "reward_type_id", DbType = DbType.Int32, Value = objRewardEntity.reward_type_id };
            var paramstart_date = new SqlParameter { ParameterName = "start_date", DbType = DbType.DateTime, Value = objRewardEntity.start_date };
            var paramend_date = new SqlParameter { ParameterName = "end_date", DbType = DbType.DateTime, Value = objRewardEntity.end_date };
            var paramstatus = new SqlParameter { ParameterName = "status", DbType = DbType.Int32, Value = objRewardEntity.status };
            var paramdescription = new SqlParameter { ParameterName = "description", DbType = DbType.String, Value = objRewardEntity.description ?? (object)DBNull.Value };
            var paramsent_mail = new SqlParameter { ParameterName = "sent_mail", DbType = DbType.Boolean, Value = objRewardEntity.sent_mail };
            var paramdisplay_dashborad = new SqlParameter { ParameterName = "display_dashborad", DbType = DbType.Boolean, Value = objRewardEntity.display_dashborad };
            var paramcreated_by = new SqlParameter { ParameterName = "created_by", DbType = DbType.Int32, Value = objRewardEntity.created_by };
            var paramisAddMode = new SqlParameter { ParameterName = "isAddMode", DbType = DbType.Int32, Value = isAddMode };
            var parammodified_by = new SqlParameter { ParameterName = "modified_by", DbType = DbType.Int32, Value = objRewardEntity.modified_by ?? (object)DBNull.Value };
            var paramIsCustomerDepartment = new SqlParameter { ParameterName = "isCustomerDepartment", DbType = DbType.Boolean, Value = objRewardEntity.IsCustomerDepartment };
            int rewardId = objGenericRepository.ExecuteSQL<int>("InsertOrUpdateReward", ParameterStoreId, ParameterLoggedInUserId, paramRewardId, paramRewardTitle, paramreward_type_id, paramstart_date, paramend_date,
                             paramstatus, paramdescription, paramsent_mail, paramdisplay_dashborad, paramcreated_by, paramisAddMode, parammodified_by, paramIsCustomerDepartment).SingleOrDefault();
            return rewardId;
        }

        public int DeleteReward(int StoreId, int LoggedInUserId, int reward_id)
        {
            var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramRewardId = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = reward_id };
            int result = objGenericRepository.ExecuteSQL<int>("DeleteReward", ParameterStoreId, ParameterLoggedInUserId, paramRewardId).SingleOrDefault();
            return result;
        }

        public List<RewardUserEntity> ViewCustomerRewards(int StoreId, int LoggedInUserId, int reward_id)
        {
            var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramRewardId = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = reward_id };
            var rewardCustomerlist = objGenericRepository.ExecuteSQL<RewardUserEntity>("ViewCustomer_Reward", ParameterStoreId, ParameterLoggedInUserId, paramRewardId).ToList();
            return rewardCustomerlist;
        }

        public List<RewardUserDetailsEntity> GetRewardCustomerDetails(int StoreId, int LoggedInUserId, int reward_user_id)
        {
            var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramrewardUserId = new SqlParameter { ParameterName = "reward_user_id", DbType = DbType.Int32, Value = reward_user_id };
            var rewardCustomerDetails = objGenericRepository.ExecuteSQL<RewardUserDetailsEntity>("GetReward_CustomerDetails", ParameterStoreId, ParameterLoggedInUserId, paramrewardUserId).ToList();
            return rewardCustomerDetails;
        }

        public int DeleteRewardUser(int StoreId, int LoggedInUserId, int id, int reward_user_id)
        {
            var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramId = new SqlParameter { ParameterName = "id", DbType = DbType.Int32, Value = id };
            var paramRewardUserId = new SqlParameter { ParameterName = "reward_user_id", DbType = DbType.Int32, Value = reward_user_id };
            var deletedRecord = objGenericRepository.ExecuteSQL<int>("Delete_RewardUser", ParameterStoreId, ParameterLoggedInUserId, paramId, paramRewardUserId).SingleOrDefault();
            return deletedRecord;
        }

        public List<RewardUserDetailsEntity> MyRewards(int StoreId, int id)
        {
            var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };

            var paramId = new SqlParameter { ParameterName = "id", DbType = DbType.Int32, Value = id };
            var myRewardlist = objGenericRepository.ExecuteSQL<RewardUserDetailsEntity>("GetMyRewards", ParameterStoreId, paramId).ToList();
            return myRewardlist;
        }

        public List<RewardEntity> GetAllRunningRewards(int StoreId, int LoggedInUserId)
        {
            var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var runningAwards = objGenericRepository.ExecuteSQL<RewardEntity>("GetAllRunningRewards", ParameterStoreId, ParameterLoggedInUserId).ToList();
            return runningAwards;
        }

        public List<RewardUserEntity> RewardCustomerDetails(int StoreId, int LoggedInUserId, int reward_id)
        {
            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramReawrdId = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = reward_id };
            var rewardcustomers = objGenericRepository.ExecuteSQL<RewardUserEntity>("RewardCustomerDetails", ParameterStoreId, ParameterLoggedInUserId, paramReawrdId).ToList();
            return rewardcustomers;
        }

        public List<AddRewardEntity> GetRewardGroupCustomers(int StoreId, int loginUserId, int reward_id)
        {
            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };
            var paramRewardId = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = reward_id };
            var paramLoginUserId = new SqlParameter { ParameterName = "loginUserid", DbType = DbType.Int32, Value = loginUserId };
            var rewardGroupCustomers = objGenericRepository.ExecuteSQL<AddRewardEntity>("GetReward_GroupCustomers", ParameterStoreId, paramRewardId, paramLoginUserId).ToList();
            return rewardGroupCustomers;
        }

        public int AddCustomer_Reward(int StoreId, int LoggedInUserId, AddRewardEntity objAddRewardEntity)
        {
            if (objAddRewardEntity.reward_type_id == Convert.ToInt32(SystemEnum.RewardType.Star))
            {
                objAddRewardEntity.reward_name = "Star " + Convert.ToString(objAddRewardEntity.NoOfStars);
            }
            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramRewardId = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = objAddRewardEntity.reward_id };
            var paramISWinner = new SqlParameter { ParameterName = "IsWinner", DbType = DbType.Boolean, Value = false };
            var paramCustomerId = new SqlParameter { ParameterName = "reward_giventouser_id", DbType = DbType.Int32, Value = objAddRewardEntity.customer_id };
            var paramLoginUserId = new SqlParameter { ParameterName = "reward_givenby_userid", DbType = DbType.Int32, Value = objAddRewardEntity.loginUserid };
            var paramRewardName = new SqlParameter { ParameterName = "reward_name", DbType = DbType.String, Value = objAddRewardEntity.reward_name };
            var paramRewardTypeId = new SqlParameter { ParameterName = "reward_type_id", DbType = DbType.Int32, Value = objAddRewardEntity.reward_type_id };
            var paramComment = new SqlParameter { ParameterName = "comment", DbType = DbType.String, Value = objAddRewardEntity.Comment ?? (object)DBNull.Value };
            int reward_inserted_id = objGenericRepository.ExecuteSQL<int>("AddCustomer_Reward", ParameterStoreId, ParameterLoggedInUserId, paramRewardId, paramISWinner, paramCustomerId, paramLoginUserId,
                paramRewardName, paramRewardTypeId, paramComment).SingleOrDefault();
            return reward_inserted_id;
        }

        public int Declare_RewardWinner(int StoreId, int LoggedInUserId, int reward_id, int reward_user_id, string admin_comment)
        {
            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramReward_id = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = reward_id };
            var paramReward_user_id = new SqlParameter { ParameterName = "reward_user_id", DbType = DbType.Int32, Value = reward_user_id };
            var paramAdminComment = new SqlParameter { ParameterName = "admin_comment", DbType = DbType.String, Value = admin_comment ?? (object)DBNull.Value };
            int result = objGenericRepository.ExecuteSQL<int>("Declare_RewardWinner", ParameterStoreId, ParameterLoggedInUserId, paramReward_id, paramReward_user_id, paramAdminComment).SingleOrDefault();
            return result;
        }

        public int Delete_RewardWinner(int StoreId, int LoggedInUserId, int reward_user_id)
        {
            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramReward_user_id = new SqlParameter { ParameterName = "reward_user_id", DbType = DbType.Int32, Value = reward_user_id };
            int result = objGenericRepository.ExecuteSQL<int>("Delete_RewardWinner", ParameterStoreId, ParameterLoggedInUserId, paramReward_user_id).SingleOrDefault();
            return result;
        }

        public List<RewardUserEntity> Dashboard_RewardWinner(int StoreId, int LoggedInUserId)
        {
            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var winners = objGenericRepository.ExecuteSQL<RewardUserEntity>("Dashboard_RewardWinner", ParameterStoreId, ParameterLoggedInUserId).ToList();
            return winners;
        }
    }
}
