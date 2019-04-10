 using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
   public interface IUserServices
    {
       List<UserEntity> GetUserList(int LoggedInUserId,int storeId);

       UserEntity GetUserDetail(int LoggedInUserId,int storeId, int UserID = 0);

       int CreateOrUpdateUser(int LoggedInUserId,int storeId, UserEntity user);
       int DeleteUserDetail(int LoggedInUserId,int storeId,int UserId);
       UserEntity AuthenticUserDetail(UserEntity model);
    }
}
