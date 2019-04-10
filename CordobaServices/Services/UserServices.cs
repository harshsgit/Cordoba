
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
    public class UserServices : IUserServices
    {
        private GenericRepository<UserEntity> UserEntityGenericRepository = new GenericRepository<UserEntity>();
        public List<UserEntity> GetUserList(int LoggedInUserId, int storeId)
        {
            try
            {
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = storeId
                };

                var UserList = UserEntityGenericRepository.ExecuteSQL<UserEntity>("EXEC GetUserList", ParameterLoggedInUserId, ParameterStoreId).ToList<UserEntity>().ToList();
                return UserList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserEntity GetUserDetail(int LoggedInUserId, int storeId, int userID = 0)
        {
            UserEntity UserDetail = new UserEntity();
            if (userID > 0)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[]{
                     new SqlParameter("StoreId", (object)DBNull.Value),
                     new SqlParameter("LoggedInUserId", (object)DBNull.Value),
                     new SqlParameter("user_id", userID)
                    };
                    UserDetail = UserEntityGenericRepository.ExecuteSQL<UserEntity>("EXEC GetUserDetail", param).ToList<UserEntity>().FirstOrDefault();

                }
                catch (Exception)
                {

                    throw;
                }

            }

            return UserDetail;

        }


        public int CreateOrUpdateUser(int LoggedInUserId, int storeId, UserEntity user)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[12];
                param[0] = new SqlParameter("LoggedInUserId", LoggedInUserId);
                param[1] = new SqlParameter("storeId", storeId);
                param[2] = new SqlParameter("user_id", user.user_id);
                param[3] = new SqlParameter("user_group_id", user.user_group_id);
                param[4] = new SqlParameter("username", user.username);
                param[5] = new SqlParameter("password", user.password);
                param[6] = new SqlParameter("firstname", user.firstname);
                param[7] = new SqlParameter("lastname", user.lastname);
                param[8] = new SqlParameter("email", user.email != null ? user.email : (object)DBNull.Value);
                param[9] = new SqlParameter("status", user.status);
                param[10] = new SqlParameter("ip", user.ip != null ? user.ip : (object)DBNull.Value);
                param[11] = new SqlParameter("image", user.image != null ? user.image : (object)DBNull.Value);

                var result = UserEntityGenericRepository.ExecuteSQL<int>("EXEC InsertOrUpdateUser", param).ToList<int>().FirstOrDefault();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int DeleteUserDetail(int LoggedInUserId, int storeId, int UserId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("LoggedInUserId", LoggedInUserId);
                param[1] = new SqlParameter("storeId", storeId);
                param[2] = new SqlParameter("user_id", UserId);
                var Result = UserEntityGenericRepository.ExecuteSQL<int>("EXEC DeleteUser", param).FirstOrDefault();
                return Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserEntity AuthenticUserDetail(UserEntity model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]{
                     new SqlParameter("Email", !string.IsNullOrWhiteSpace(model.email)?(object)model.email:(object)DBNull.Value),
                     new SqlParameter("Password",!string.IsNullOrWhiteSpace(model.password)? (object)model.password :(object)DBNull.Value)
                    };
                var Result = UserEntityGenericRepository.ExecuteSQL<UserEntity>("EXEC GetAuthenticUserDetail", param).FirstOrDefault();
                return Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public UserEntity GetAuthenticUserDetail(UserEntity model)
        //{
        //    try
        //    {
        //        SqlParameter[] param = new SqlParameter[2];
        //        param[0] = new SqlParameter("Email", model.email);
        //        param[1] = new SqlParameter("Password", model.password);
        //        var Result = UserEntityGenericRepository.ExecuteSQL<UserEntity>("EXEC GetAuthenticUserDetail", param).FirstOrDefault();
        //        return Result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
