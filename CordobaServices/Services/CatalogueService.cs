using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class CatalogueService : ICatalogueServices
    {
        private GenericRepository<CatalogueEntity> objGenericRepository = new GenericRepository<CatalogueEntity>();

        public List<CatalogueEntity> GetCatalogueList(int StoreId, int LoggedInUserId)
        {
            List<CatalogueEntity> Catalogue = new List<CatalogueEntity>();
            try
            {
                var paramStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var paramLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserIdId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var Result = objGenericRepository.ExecuteSQL<CatalogueEntity>("GetCatalogueList", paramStoreId, paramLoggedInUserId).ToList<CatalogueEntity>();

                if (Result != null)
                    Catalogue = Result.ToList();

            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
            }
            return Catalogue;
        }

        public CatalogueEntity GetCatalogueById(int StoreId, int LoggedInUserId, int CatalogueId = 0)
        {
            CatalogueEntity ProductCatalogueEntity = new CatalogueEntity();
            if (CatalogueId > 0)
            {
                var paramCatalogueId = new SqlParameter
                {
                    ParameterName = "CatalogueId",
                    DbType = DbType.Int32,
                    Value = CatalogueId
                };
                var paramStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var paramLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserIdId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var result = objGenericRepository.ExecuteSQL<CatalogueEntity>("GetCatalogueById", paramCatalogueId, paramStoreId, paramLoggedInUserId).FirstOrDefault();
                ProductCatalogueEntity = result;
            }
            else
            {
                ProductCatalogueEntity = new CatalogueEntity();
            }

            return ProductCatalogueEntity;

        }


        public int InsertUpdateCatalogue(int StoreId, int LoggedInUserId, CatalogueEntity catalogueEntity)
        {
            var catalogueIdparam = new SqlParameter
            {
                ParameterName = "catalogue_Id",
                DbType = DbType.Int32,
                Value = catalogueEntity.catalogue_Id
            };

            var nameparam = new SqlParameter
            {
                ParameterName = "Name",
                DbType = DbType.String,
                Value = catalogueEntity.Name
            };

            var paramStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };

            var paramLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserIdId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var result = objGenericRepository.ExecuteSQL<int>("InsertUpdateCatalogue", catalogueIdparam, nameparam, paramStoreId, paramLoggedInUserId).FirstOrDefault();
            return result;
        }


        public int DeleteCatalogue(int catalogue_id, int StoreId, int LoggedInUserId)
        {
            try
            {
                var paramCatalogueId = new SqlParameter
                {
                    ParameterName = "catalogue_id",
                    DbType = DbType.Int32,
                    Value = catalogue_id
                };

                var paramStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };

                var paramLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserIdId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };

                int result = objGenericRepository.ExecuteSQL<int>("DeleteCatalogue", paramCatalogueId, paramStoreId, paramLoggedInUserId).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ImportProductCatalogueEntity> ImportDatatoCatalogue(int StoreId, int LoggedInUserId, int supplier_id, int language_id, int catalogue_id, DataTable XLS, bool IsConfirmToIgnore)
        {
            try
            {                            
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"]);
                con.Open();
                SqlCommand cmd = new SqlCommand("ImportCatalogueDataToTable", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@importTypetbl";
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.TypeName = "dbo.ImportCatalogType2";
                parameter.Value = XLS;
                cmd.Parameters.Add(parameter);
                cmd.Parameters.AddWithValue("@Supplier_Id", supplier_id);
                cmd.Parameters.AddWithValue("@Language_Id", language_id);
                cmd.Parameters.AddWithValue("@Catalogue_Id", catalogue_id);
                cmd.Parameters.AddWithValue("@CreatedBy", LoggedInUserId);
                cmd.Parameters.AddWithValue("@IsConfirmToIgnore", IsConfirmToIgnore);
                cmd.CommandTimeout = 120000000;

                System.Data.IDataReader dr = cmd.ExecuteReader();
                List<ImportProductCatalogueEntity> lstEntity = new List<ImportProductCatalogueEntity>();

                while (dr.Read())
                {
                    ImportProductCatalogueEntity objEntity = new ImportProductCatalogueEntity();
                    objEntity.code = Convert.ToString(dr.GetValue(0));
                    objEntity.name = Convert.ToString(dr.GetValue(1));
                    lstEntity.Add(objEntity);
                    //result = Convert.ToInt32(dr[0]);
                }

                dr.Close();


                //tvpParam.TypeName = "dbo.ImportCatalogType";
                //    int result = 0;
                //result = cmd.ExecuteNonQuery();
                //  result = Convert.ToInt32(cmd.ExecuteScalar());

                //System.Data.IDataReader dr = cmd.ExecuteReader();
                //while (dr.Read())
                //{
                //    result = Convert.ToInt32(dr[0]);
                //}
                //dr.Close();
                con.Close();
                return lstEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CategoryEntity> GetCategoryListForStore(int StoreId)
        {
            List<CategoryEntity> category = new List<CategoryEntity>();
            try
            {
                var paramStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };

                var Result = objGenericRepository.ExecuteSQL<CategoryEntity>("GetCategoryListForStore", paramStoreId).ToList<CategoryEntity>();

                if (Result != null)
                    category = Result.ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return category;
        }


    }
}
