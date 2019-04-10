using CordobaModels.Entities;
using CordobaServices.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
   public interface IStoreServices
    {
       List<StoreEntity> GetStoreList(int? StoreID, int LoggedInUserId);

       StoreEntity GetStoreById(int store_id, int LoggedInUserId);

       int InsertUpdateStore(StoreEntity storeEntity, int LoggedInUserId);

       int? DeleteStoreById_Admin(int storeId, int LoggedInUserId);

       bool UploadStoreImage(int Store_Id, string ImageName, int ImageKey,int layout);

       bool UploadStoreLogo(int store_id, string logo);

       List<StoreImageEntity> GetAdvertisementImageList(int StoreId);

       StoreHTMLEntity GetStoreHTMLCharts(int StoreID, int Month, int Year);

       
    }
}
