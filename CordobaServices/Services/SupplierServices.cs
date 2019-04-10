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
    public class SupplierServices : ISupplierServices
    {
        private GenericRepository<SupplierEntity> objGenericRepository = new GenericRepository<SupplierEntity>();

        public List<SupplierEntity> GetSupplierList(int storeId, int LoggedInUserId, int? SupplierID)
        {
            List<SupplierEntity> SupplierList = new List<SupplierEntity>();
            var paramSupplierId = new SqlParameter { ParameterName = "supplierId", DbType = DbType.Int32, Value = SupplierID };
            var ParameterStoreId = new SqlParameter { ParameterName = "StoreId ", DbType = DbType.Int32, Value = storeId };
            var ParameterLoggedInUserId = new SqlParameter { ParameterName = "LoggedInUserId", DbType = DbType.Int32, Value = LoggedInUserId};

            SupplierList = objGenericRepository.ExecuteSQL<SupplierEntity>("GetSupplierList", paramSupplierId, ParameterStoreId, ParameterLoggedInUserId).ToList();            
            return SupplierList;
        }

        public int InsertOrUpdateSupplier(int storeId, int LoggedInUserId, SupplierEntity objSupplier)
        {
            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "storeId",
                DbType = DbType.Int32,
                Value = storeId
            };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramSupplierId = new SqlParameter { ParameterName = "supplierId", DbType = DbType.Int32, Value = Convert.ToInt32(objSupplier.supplier_id) };
            var paramSupplierName = new SqlParameter { ParameterName = "supplierName", DbType = DbType.String, Value = objSupplier.name.Trim() ?? (object)DBNull.Value };
            var paramSupplierAddress = new SqlParameter { ParameterName = "supplierAddress", DbType = DbType.String, Value = objSupplier.address ?? (object)DBNull.Value };
            try
            {
                int result = objGenericRepository.ExecuteSQL<int>("InsertOrUpdateSupplier", ParameterStoreId, paramSupplierId, paramSupplierName, paramSupplierAddress).FirstOrDefault();
                return result;
            }
            
            catch(Exception e)
            {
                return 0;
            }
        }

        public SupplierEntity GetSupplierDetail(int storeId, int LoggedInUserId, int? SupplierID)
        {
            SupplierEntity Supplier = new SupplierEntity();
            if (SupplierID > 0)
            {
                Supplier = (from t in GetSupplierList(storeId, LoggedInUserId, SupplierID)
                            where t.supplier_id == SupplierID
                            select t).FirstOrDefault();
            }
            else
            {
                Supplier = new SupplierEntity();
            }

            return Supplier;

        }

        public int DeleteSupplier(int storeId, int LoggedInUserId, int supplierId)
        {
            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "storeId",
                DbType = DbType.Int32,
                Value = storeId
            };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramSupplierId = new SqlParameter { ParameterName = "supplierId", DbType = DbType.Int32, Value = supplierId };
            int result = objGenericRepository.ExecuteSQL<int>("DeleteSupplier", paramSupplierId).FirstOrDefault();
            return result;
        }
    }
}
