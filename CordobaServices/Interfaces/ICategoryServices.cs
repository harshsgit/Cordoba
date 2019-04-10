using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ICategoryServices
    {
        List<CategoryEntity> GetCategoryList(int StoreId, int LoggedInUserId, int Category_Id = 0);

        CategoryEntity GetCategoryById(int Category_Id, int StoreId, int LoggedInUserId);


        //Popular Category
        List<CategoryPopularEntity> GetCategoryListByStoreIdPopular(int LoggedInUserId, int storeID = 0);

        List<StoreEntity> GetStoreNameList(int StoreId, int LoggedInUserId);

        int InsertOrUpdateCategoryAsPopular(int LoggedInUserId, CategoryPopularEntity categoryPopular);

        int InsertOrUpdateCategory(int StoreId, int LoggedInUserId, CategoryEntity categoryEntity);

        List<CategoryEntity> GetParentCategoryList(int StoreId, int LoggedInUserId);
        //Get Language

        //Delete Category
        int DeleteCategory(int Category_Id, int StoreId, int LoggedInUserId);

        List<LanguageEntity> GetLanguageList(int StoreId, int LoggedInUserId);

        List<ReportCategoryEntity> GetReportCategories();

        bool UpdateCategoryImage(int Category_Id, string fileName);

    }
}
