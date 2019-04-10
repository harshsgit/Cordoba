using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ICustomerGroupService
    {
        List<CustomerGroupEntity> GetCustomerGroupList(int StoreId, int LoggedInUserId);
        CustomerGroupEntity GetCustomerGroupDetail(int StoreId, int LoggedInUserId, int currencyID = 0);

        int CreateOrUpdateCustomerGroup(int StoreId, int LoggedInUserId, CustomerGroupEntity customerGroup);

        int DeleteCustomerGroup(int StoreId, int LoggedInUserId, int CustomerGroupId);
    }
}
