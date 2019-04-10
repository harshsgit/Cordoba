using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
   public interface IManufacturerServices
    {
        List<ManufacturersEntity> GetManufacturersList(int StoreId, int LoggedInUserId);

        ManufacturersEntity GetManufaturerDetail(int StoreId, int LoggedInUserId, int ManufacturersID);

         int InsertUpdateManufacture(int StoreId, int LoggedInUserId, ManufacturersEntity manufacturersEntity);

         int DeleteManufacturer(int StoreId, int LoggedInUserId, int manufacturer_id);
    }
}
