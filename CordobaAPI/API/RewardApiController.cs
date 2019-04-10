using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class RewardApiController : ApiController
    {
        public IRewardService _rewardService;
        public RewardApiController()
        {
            _rewardService = new RewardService();
        }

        [HttpGet]
        public HttpResponseMessage GetRewardList(int StoreId, int LoggedInUserId, int reward_id)
        {
            try
            {
                var result = _rewardService.GetRewardList( StoreId,  LoggedInUserId, reward_id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetRewardTypeList(int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _rewardService.GetRewardTypeList(StoreId,  LoggedInUserId);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage InsertOrUpdateReward(int StoreId, int LoggedInUserId, int isAddMode, RewardEntity objRewardEntity)
        {
            try
            {
                var result = _rewardService.InsertOrUpdateReward(StoreId, LoggedInUserId, objRewardEntity, isAddMode);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteReward(int StoreId, int LoggedInUserId, int reward_id)
        {
            try
            {
                int result = _rewardService.DeleteReward(StoreId, LoggedInUserId, reward_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage ViewCustomerRewards(int StoreId, int LoggedInUserId, int reward_id)
        {
            try
            {
                var customerRewards = _rewardService.ViewCustomerRewards(StoreId, LoggedInUserId, reward_id);
                return Request.CreateResponse(HttpStatusCode.OK, customerRewards);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetRewardCustomerDetails(int StoreId, int LoggedInUserId, int reward_user_id)
        {
            try
            {
                var customer_rewarddetails = _rewardService.GetRewardCustomerDetails(StoreId, LoggedInUserId, reward_user_id);
                return Request.CreateResponse(HttpStatusCode.OK, customer_rewarddetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteRewardUser(int StoreId, int LoggedInUserId, int id, int reward_user_id)
        {
            try
            {
                var result = _rewardService.DeleteRewardUser(StoreId, LoggedInUserId, id, reward_user_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage MyRewards(int StoreId,int id)
        {
            try
            {
                var myRewardslist = _rewardService.MyRewards(StoreId,id);
                return Request.CreateResponse(HttpStatusCode.OK, myRewardslist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAllRunningRewards(int StoreId, int LoggedInUserId)
        {
            try
            {
                var allrunningRewards = _rewardService.GetAllRunningRewards(StoreId,  LoggedInUserId);
                return Request.CreateResponse(HttpStatusCode.OK, allrunningRewards);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage RewardCustomerDetails(int StoreId, int LoggedInUserId,int reward_id)
        {
            try
            {
                var customerRewards = _rewardService.RewardCustomerDetails(StoreId, LoggedInUserId, reward_id);
                return Request.CreateResponse(HttpStatusCode.OK, customerRewards);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetRewardGroupCustomers(int StoreId, int loginUserId, int reward_id)
        {
            try
            {
                var rewardGroupCustomers = _rewardService.GetRewardGroupCustomers(StoreId, loginUserId, reward_id);
                return Request.CreateResponse(HttpStatusCode.OK, rewardGroupCustomers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage AddCustomer_Reward(int StoreId, int LoggedInUserId, AddRewardEntity objAddrewardEntity)
        {
            try
            {
                var insertedId = _rewardService.AddCustomer_Reward(StoreId, LoggedInUserId, objAddrewardEntity);
                return Request.CreateResponse(HttpStatusCode.OK, insertedId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage Declare_RewardWinner(int StoreId, int LoggedInUserId, int reward_id, int reward_user_id, string admin_comment)
        {
            try
            {
                var result = _rewardService.Declare_RewardWinner(StoreId, LoggedInUserId, reward_id, reward_user_id, admin_comment);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage Delete_RewardWinner(int StoreId, int LoggedInUserId, int reward_user_id)
        {
            try
            {
                var result= _rewardService.Delete_RewardWinner(StoreId, LoggedInUserId, reward_user_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage Dashboard_RewardWinner(int StoreId, int LoggedInUserId)
        {
            try
            {
                var listWinners = _rewardService.Dashboard_RewardWinner(StoreId, LoggedInUserId);
                return Request.CreateResponse(HttpStatusCode.OK, listWinners);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
