using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ISupplierServices
    {
        List<SupplierEntity> GetSupplierList(int storeId, int LoggedInUserId, int? SupplierID);
        SupplierEntity GetSupplierDetail(int storeId, int LoggedInUserId, int? SupplierID);
        int InsertOrUpdateSupplier(int storeId, int LoggedInUserId, SupplierEntity objSupplier);
        int DeleteSupplier(int storeId, int LoggedInUserId, int supplierId);
    }
}
