using CordobaModels.Entities;
using CordobaServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerEntity> GetCustomerList(string sortColumn, TableParameter<CustomerEntity> tableParameter, string customerName, string email, int? customer_group_id,int? status, int? approved, string ip, DateTime? date_added , int? storeId);
        
        CustomerEntity GetCustomerById(int StoreId, int LoggedInUserId, int customer_id);

        int InsertUpdateCustomer(int LoggedInUserId, CustomerEntity customerEntity);

        int DeleteCustomer(int StoreId, int LoggedInUserId, int customer_id);

        string CustomerImport(int store_id,int LoggedInUserId, int customer_group_id, DataTable CustomerTable, string UserPassword);

        List<PointsAuditEntity> PointsImporter(int store_id, int LoggedInUserId, bool IsSendEmail, DataTable PointsTable);

        bool UploadUserImage(int customerImage_id, int customer_id, string ImageName);

        List<CustomerImageEntity> getUserImage(int customer_id);

        int deleteCustomerImage(int banner_id);

        int InsertPointAudit(int customer_id, string description, int points);

        CustomerEntity SendCustomerPassword(int customer_id, string NewPassword);
        
        DataSet CustomerExportToExcel(string sortColumn, object filter, string customerName, string email, int? customer_group_id, int? status, int? approved, string ip, DateTime? date_added, int? storeId);
      
    }
}
