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
    public class CategoryService : ICategoryServices
    {
        private GenericRepository<CategoryEntity> CategoryEntityGenericRepository = new GenericRepository<CategoryEntity>();
        private GenericRepository<CategoryPopularEntity> CategoryPopularEntityGenericRepository = new GenericRepository<CategoryPopularEntity>();
        private GenericRepository<LanguageEntity> LanguageEntityGenericRepository = new GenericRepository<LanguageEntity>();
        private GenericRepository<ReportCategoryEntity> ReportCategoryEntityGenericRepository = new GenericRepository<ReportCategoryEntity>();

        public List<CategoryEntity> GetCategoryList(int StoreId, int LoggedInUserId, int Category_Id = 0)
        
        {
            List<CategoryEntity> Categories = new List<CategoryEntity>();

            var ParameterCategoryId = new SqlParameter
            {
                ParameterName = "Category_Id",
                DbType = DbType.Int32,
                Value = Category_Id
            };
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
            var catalogueResult = CategoryEntityGenericRepository.ExecuteSQL<CategoryEntity>("GetCategoryList", ParameterStoreId, ParameterLoggedInUserId, ParameterCategoryId).ToList<CategoryEntity>();
            if (catalogueResult != null)
                Categories = catalogueResult.ToList();
            return Categories;
        }



        public CategoryEntity GetCategoryById(int Category_Id, int StoreId, int LoggedInUserId)
        {
            CategoryEntity CategoryEntity = new CategoryEntity();
            List<CategoryDescriptionList> CategoryDescriptionList = new List<CategoryDescriptionList>();
            List<StoreEntity> StoreList = new List<StoreEntity>();

            if (Category_Id > 0)
            {
                var paramCategoryId = new SqlParameter 
                { 
                    ParameterName = "Category_Id", 
                    DbType = DbType.Int32, 
                    Value = Category_Id 
                };
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
                var result = CategoryEntityGenericRepository.ExecuteSQL<CategoryEntity>("GetCategoryById", paramCategoryId, ParameterStoreId, ParameterLoggedInUserId).FirstOrDefault();
                CategoryEntity = result;

                var paramCategoryIdForDesc = new SqlParameter 
                { 
                    ParameterName = "Category_Id", 
                    DbType = DbType.Int32, 
                    Value = Category_Id 
                };
                var DescResult = CategoryEntityGenericRepository.ExecuteSQL<CategoryDescriptionList>("GetCategoryDescriptionList", paramCategoryIdForDesc).ToList<CategoryDescriptionList>();
                if (DescResult != null)
                    CategoryDescriptionList = DescResult.ToList();
            }
            else
            {
                CategoryEntity = new CategoryEntity();
                CategoryDescriptionList = new List<CategoryDescriptionList>();
                CategoryEntity.status = 1;
            }

            var paramCategoryIdForCategory = new SqlParameter
            {
                ParameterName = "Category_Id",
                DbType = DbType.Int32,
                Value = Category_Id
            };
            var storeResult = CategoryEntityGenericRepository.ExecuteSQL<StoreEntity>("GetCategoryToStoreList", paramCategoryIdForCategory).ToList<StoreEntity>();
            if (storeResult != null)
                StoreList = storeResult.ToList();

            CategoryEntity.CategoryDescriptionList = CategoryDescriptionList;
            CategoryEntity.StoreList = StoreList;
            return CategoryEntity;
        }


        //Popular Category

        public List<CategoryPopularEntity> GetCategoryListByStoreIdPopular(int LoggedInUserId, int storeID = 0)
        {
            List<CategoryPopularEntity> PopularCategoryList = new List<CategoryPopularEntity>();
            if (storeID >= 0)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("StoreId", storeID);
                    param[1] = new SqlParameter("LoggedInUserId", LoggedInUserId);
                    PopularCategoryList = CategoryEntityGenericRepository.ExecuteSQL<CategoryPopularEntity>("EXEC GetCategoryListByStoreIdPopular", param).ToList<CategoryPopularEntity>().ToList();

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return PopularCategoryList;
        }


        public List<StoreEntity> GetStoreNameList(int StoreId, int LoggedInUserId)
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
                var StoreList = CategoryEntityGenericRepository.ExecuteSQL<StoreEntity>("EXEC GetStoreNameList", ParameterStoreId, ParameterLoggedInUserId).ToList<StoreEntity>().ToList();
                return StoreList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int InsertOrUpdateCategoryAsPopular(int LoggedInUserId, CategoryPopularEntity categoryPopular)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("category_popularId", categoryPopular.category_popularId!=null ?categoryPopular.category_popularId:(object)DBNull.Value)
                    ,new SqlParameter("category_Id", categoryPopular.category_Id)
                    ,new SqlParameter("store_Id", categoryPopular.store_Id)
                    ,new SqlParameter("LoggedInUserId", LoggedInUserId)
                    ,new SqlParameter("createdby", categoryPopular.createdby)
                };


                var result = CategoryPopularEntityGenericRepository.ExecuteSQL<int>("EXEC InsertOrUpdateCategoryAsPopular", param).ToList<int>().FirstOrDefault();



                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        // Language get


        public List<LanguageEntity> GetLanguageList(int StoreId, int LoggedInUserId)
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
                var languageEntity = LanguageEntityGenericRepository.ExecuteSQL<LanguageEntity>("EXEC GetLanguageList", ParameterStoreId, ParameterLoggedInUserId).ToList<LanguageEntity>().ToList();
                return languageEntity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<ReportCategoryEntity> GetReportCategories()
        {
            try
            {
                var reportcategoryEntity = ReportCategoryEntityGenericRepository.ExecuteSQL<ReportCategoryEntity>("EXEC GetReportCategories").ToList<ReportCategoryEntity>().ToList();
                return reportcategoryEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CategoryEntity> GetParentCategoryList(int StoreId, int LoggedInUserId)
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
                var ParentCategoryList = CategoryEntityGenericRepository.ExecuteSQL<CategoryEntity>("EXEC GetParentCategoryList", ParameterStoreId, ParameterLoggedInUserId).ToList<CategoryEntity>().ToList();
                return ParentCategoryList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //Insert Or Update Category
        public int InsertOrUpdateCategory(int StoreId, int LoggedInUserId, CategoryEntity categoryEntity)
        {
            try
            {
                string CategoryDescriptionXml = Helpers.ConvertToXml<CategoryDescriptionList>.GetXMLString(categoryEntity.CategoryDescriptionList);
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                                                   new SqlParameter("StoreId", StoreId)
                                                 , new SqlParameter("LoggedInUserId", LoggedInUserId)
                                                 , new SqlParameter("Category_Id", categoryEntity.Category_Id)
                                                 , new SqlParameter("parent_id", categoryEntity.parent_Id)
                                                 , new SqlParameter("image", categoryEntity.image !=null?categoryEntity.image:"")
                                                 , new SqlParameter("sort_order", categoryEntity.sort_order!=null?categoryEntity.sort_order:0)
                                                 , new SqlParameter("status", categoryEntity.status !=null?categoryEntity.status :1)
                                                 , new SqlParameter("StoreIdCSV", categoryEntity.StoreIdCSV !=null?categoryEntity.StoreIdCSV:"")
                                                 , new SqlParameter("CategoryDescriptionXml", CategoryDescriptionXml !=null?CategoryDescriptionXml:"")
                                                 , new SqlParameter("ReportCategoryId",(object) categoryEntity.ReportCategoryId ?? DBNull.Value)
                                                };

                int result = CategoryEntityGenericRepository.ExecuteSQL<int>("InsertOrUpdateCategory", sqlParameter).FirstOrDefault();
                return result;
            }
            catch(Exception e)
            {
                return 0;
            }
        }


        //Delete Category
        public int DeleteCategory(int Category_Id, int StoreId, int LoggedInUserId)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                                                   new SqlParameter("Category_Id", Category_Id)
                                                 , new SqlParameter("StoreId", StoreId)
                                                 , new SqlParameter("LoggedInUserId", LoggedInUserId) };

                int result = CategoryEntityGenericRepository.ExecuteSQL<int>("DeleteCategory", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCategoryImage(int Category_Id, string fileName)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                    new SqlParameter("Category_Id", Category_Id),
                    new SqlParameter("ImageName", !string.IsNullOrWhiteSpace(fileName)?(object)fileName:(object)DBNull.Value)
                };
                int result = CategoryEntityGenericRepository.ExecuteSQL<int>("UpdateCategoryImage", sqlParameter).FirstOrDefault();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
