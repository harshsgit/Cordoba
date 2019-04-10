using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ICustomerDepartmentService
    {
        List<CustomerDepartmentEntity> GetCustomerDepartmentList(int storeId);
        List<CustomerDepartmentEntity> GetCustomerDepartmentById(int CustomerDepartmentId);
        int DeleteCustomerDepartment(int CustomerDepartmentId, int StoreId);
        int InsertOrUpdateCustomerDepartment(CustomerDepartmentEntity objCustomerDepartmentEntity);
    }
}
