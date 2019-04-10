using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
   public  interface IBannerServices
    {

       List<BannerEntity> GetBannerList();
       BannerEntity GetBannerById(int bannerId);
       List<BannerEntity> GetBannerImageById(int bannerId);
       //bool UploadBannerImage(int banner_id , int banner_image_id , string link , int sort_order, string ImageName, int ImageKey);
       long UploadBannerImage(int banner_id, int banner_image_id, string link, int sort_order, string ImageName, string ImageFileName ,int ImageKey);
       int DeleteBannerImage(int banner_image_id);

       int InsertUpdateBanner(int banner_id, string name , int status);

       int DeleteBanner(int banner_id);

       //BannerEntity GetBannerById(int BannerId, int StoreId, int LoggedInUserId);
    }
}
