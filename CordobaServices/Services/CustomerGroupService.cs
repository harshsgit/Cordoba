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
    public class CustomerGroupService : ICustomerGroupService
    {
        private GenericRepository<CustomerGroupEntity> CustomerGroupEntityGenericRepository = new GenericRepository<CustomerGroupEntity>();

        public List<CustomerGroupEntity> GetCustomerGroupList(int StoreId, int LoggedInUserId)
        {
            try
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
                var CustomerGroupList = CustomerGroupEntityGenericRepository.ExecuteSQL<CustomerGroupEntity>("EXEC GetCustomerGroupList", ParameterStoreId, ParameterLoggedInUserId).ToList<CustomerGroupEntity>().ToList();
                return CustomerGroupList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public CustomerGroupEntity GetCustomerGroupDetail(int StoreId, int LoggedInUserId = 0, int customerGroupID = 0)
        {
            CustomerGroupEntity CustomerGroupDetail = new CustomerGroupEntity();
            if (customerGroupID > 0)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("StoreId", StoreId),
                    new SqlParameter("LoggedInUserId", LoggedInUserId),
                    new SqlParameter("customer_group_id", customerGroupID)
                };
                    CustomerGroupDetail = CustomerGroupEntityGenericRepository.ExecuteSQL<CustomerGroupEntity>("GetCustomerGroupDetails", param).ToList<CustomerGroupEntity>().FirstOrDefault();

                }
                catch (Exception ex)
                {

                    throw;
                }

            }

            return CustomerGroupDetail;

        }

        public int CreateOrUpdateCustomerGroup(int StoreId, int LoggedInUserId, CustomerGroupEntity customerGroup)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("store_id", StoreId);
                param[1] = new SqlParameter("LoggedInUserId", LoggedInUserId);
                param[2] = new SqlParameter("customer_group_id", customerGroup.customer_group_id);
                param[3] = new SqlParameter("name", customerGroup.name);

                var result = CustomerGroupEntityGenericRepository.ExecuteSQL<int>("EXEC InsertOrUpdateCustomerGroup", param).ToList<int>().FirstOrDefault();



                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int DeleteCustomerGroup(int StoreId, int LoggedInUserId, int CustomerGroupId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("store_id", StoreId);
                param[1] = new SqlParameter("LoggedInUserId", LoggedInUserId);
                param[2] = new SqlParameter("customer_group_id", CustomerGroupId);

                var Result = CustomerGroupEntityGenericRepository.ExecuteSQL<int>("EXEC DeleteCustomerGroup ", param).ToList<int>().FirstOrDefault();
                return Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
