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
    public class CustomerDepartmentService : ICustomerDepartmentService
    {
        private GenericRepository<CustomerDepartmentEntity> objGenericRepository = new GenericRepository<CustomerDepartmentEntity>();
        public List<CustomerDepartmentEntity> GetCustomerDepartmentList(int storeId)
        {
            List<CustomerDepartmentEntity> CustomerDepartments = new List<CustomerDepartmentEntity>();
            var parameterStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = storeId
            };
            CustomerDepartments = objGenericRepository.ExecuteSQL<CustomerDepartmentEntity>("GetCustomerDepartmentList", parameterStoreId).ToList();
            return CustomerDepartments;
        }

        public List<CustomerDepartmentEntity> GetCustomerDepartmentById(int CustomerDepartmentId)
        {
            List<CustomerDepartmentEntity> objCustomerDepartmentEntity = new List<CustomerDepartmentEntity>();
            var parameterStoreId = new SqlParameter
            {
                ParameterName = "CustomerDepartmentId",
                DbType = DbType.Int32,
                Value = CustomerDepartmentId
            };
            objCustomerDepartmentEntity = objGenericRepository.ExecuteSQL<CustomerDepartmentEntity>("GetCustomerDepartmentById", parameterStoreId).ToList();
            return objCustomerDepartmentEntity;

        }


        public int InsertOrUpdateCustomerDepartment(CustomerDepartmentEntity objCustomerDepartmentEntity)
        {
            var paramCustomerDepartmentId = new SqlParameter
            {
                ParameterName = "CustomerDepartmentId",
                DbType = DbType.Int32,
                Value = Convert.ToInt32(objCustomerDepartmentEntity.customer_department_id)
            };
            var paramStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = objCustomerDepartmentEntity.store_id
            };
            var paramDepartmentName = new SqlParameter
            {
                ParameterName = "DepartmentName",
                DbType = DbType.String,
                Value = objCustomerDepartmentEntity.name
            };

            var paramLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = objCustomerDepartmentEntity.LoggedInUserId
            };
            var ParameterStatus = new SqlParameter
            {
                ParameterName = "status",
                DbType = DbType.Int32,
                Value = objCustomerDepartmentEntity.status
            };



            int result = objGenericRepository.ExecuteSQL<int>("InsertOrUpdateCustomerDepartment", paramCustomerDepartmentId, paramStoreId, paramDepartmentName, paramLoggedInUserId, ParameterStatus).FirstOrDefault();
            return result;
        }

        public int DeleteCustomerDepartment(int CustomerDepartmentId, int StoreId)
        {
            var ParamCustomerDepartmentId = new SqlParameter
            {
                ParameterName = "CustomerDepartmentId",
                DbType = DbType.Int32,
                Value = CustomerDepartmentId
            };
            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };
            int result = objGenericRepository.ExecuteSQL<int>("DeleteCustomerDepartment", ParamCustomerDepartmentId, ParameterStoreId).FirstOrDefault();
            return result;
        }




    }
}
