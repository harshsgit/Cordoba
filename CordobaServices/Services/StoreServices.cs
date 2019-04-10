using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Helpers;
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
    public class StoreServices : IStoreServices
    {
        private GenericRepository<StoreEntity> objGenericRepository = new GenericRepository<StoreEntity>();

        public List<StoreEntity> GetStoreList(int? StoreID, int LoggedInUserId)
        {
            List<StoreEntity> StoreList = new List<StoreEntity>();
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramStoreId = new SqlParameter { ParameterName = "StoreId", DbType = DbType.Int32, Value = (StoreID == null ? 0 : StoreID) };
            StoreList = objGenericRepository.ExecuteSQL<StoreEntity>("GetStoreList", paramStoreId, ParameterLoggedInUserId).ToList();
            return StoreList;
        }


        public StoreEntity GetStoreById(int store_id, int LoggedInUserId)
        {
            StoreEntity storeEntity = new StoreEntity();
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramStoreId = new SqlParameter { ParameterName = "store_id", DbType = DbType.Int32, Value = store_id };
            var result = objGenericRepository.ExecuteSQL<StoreEntity>("GetStoreById", paramStoreId, ParameterLoggedInUserId).FirstOrDefault();
            if (result != null)
            {
                storeEntity = result;
            }
            return storeEntity;
        }


        public int InsertUpdateStore(StoreEntity storeEntity, int LoggedInUserId)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                                                    
                                                   new SqlParameter("store_id", storeEntity.store_id)
                                                 , new SqlParameter("LoggedInUserId", LoggedInUserId)
                                                 , new SqlParameter("url", storeEntity.url?? (object) DBNull.Value)
                                                 , new SqlParameter("name", storeEntity.name ??  DBNull.Value.ToString())
                                                 , new SqlParameter("title", storeEntity.title ??  DBNull.Value.ToString())
                                                 , new SqlParameter("description", storeEntity.description ??  DBNull.Value.ToString())
                                                 , new SqlParameter("terms", storeEntity.terms ??   DBNull.Value.ToString())

                                                 , new SqlParameter("address", storeEntity.address ??   DBNull.Value.ToString())
                                                 , new SqlParameter("email", storeEntity.email ??   DBNull.Value.ToString())
                                                 , new SqlParameter("telephone", storeEntity.telephone ??   DBNull.Value.ToString())
                                                 , new SqlParameter("logo", storeEntity.logo ??   DBNull.Value.ToString())

                                                 , new SqlParameter("meta_title", storeEntity.meta_title ??   DBNull.Value.ToString())
                                                 , new SqlParameter("meta_description", storeEntity.meta_description ??   DBNull.Value.ToString())
                                                 , new SqlParameter("css_overrides", storeEntity.css_overrides ??   DBNull.Value.ToString())
                                                 , new SqlParameter("template", storeEntity.template ??   DBNull.Value.ToString())
                                                 , new SqlParameter("layout", storeEntity.layout ??   DBNull.Value.ToString())
                                                 
                                                 , new SqlParameter("country_id", storeEntity.country_id)
                                                 , new SqlParameter("language", storeEntity.language  ??   DBNull.Value.ToString())
                                                 , new SqlParameter("currency", storeEntity.currency  ??   DBNull.Value.ToString())
                                                 , new SqlParameter("county", storeEntity.county      ??    DBNull.Value.ToString())
                                                 ,new SqlParameter("banner_id", storeEntity.banner_id.HasValue? storeEntity.banner_id.Value:0)
                                                 ,new SqlParameter("catalougeIdCsv", storeEntity.catalougeIdCsv ?? DBNull.Value.ToString())
                                                 ,new SqlParameter("Is_ImportPoint", storeEntity.Is_ImportPoint ?? (object)DBNull.Value)
                                                 ,new SqlParameter("Is_AccessStore", storeEntity.Is_AccessStore ?? (object)DBNull.Value)
                                                };
            int result = objGenericRepository.ExecuteSQL<int>("InsertUpdateStore", sqlParameter).FirstOrDefault();
            return result;

        }

        public int? DeleteStoreById_Admin(int storeId, int LoggedInUserId)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("StoreID", storeId)                                                 
                                                   ,new SqlParameter("LoggedInUserId", LoggedInUserId)
                                               };
            var result = objGenericRepository.ExecuteSQL<int>("DeleteStoreById_Admin", sqlParameter).FirstOrDefault();
            return result;
        }

        public bool UploadStoreImage(int Store_Id, string ImageName, int ImageKey, int layout)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                    new SqlParameter("Store_Id", Store_Id),
                    new SqlParameter("ImageName", !string.IsNullOrWhiteSpace(ImageName)?(object)ImageName:(object)DBNull.Value),
                    new SqlParameter("ImageKey", ImageKey),
                    new SqlParameter("layout", layout)
                };
                int result = objGenericRepository.ExecuteSQL<int>("UploadStoreImage", sqlParameter).FirstOrDefault();
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

        public bool UploadStoreLogo(int store_id, string logo)
        {

            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                    new SqlParameter("store_id", store_id),
                    new SqlParameter("logo",logo)
                };
                int result = objGenericRepository.ExecuteSQL<int>("UploadStoreLogo", sqlParameter).FirstOrDefault();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public List<StoreImageEntity> GetAdvertisementImageList(int StoreId)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                    new SqlParameter("store_id", StoreId),                  
                };
                var result = objGenericRepository.ExecuteSQL<StoreImageEntity>("GetAdvertisementImageList", sqlParameter).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public StoreHTMLEntity GetStoreHTMLCharts(int StoreID, int Month, int Year)
        {
            try
            {
                StoreHTMLEntity objStoreHTMLEntity = new StoreHTMLEntity();

                var objStoreImage = objGenericRepository.ExecuteSQL<StoreHTMLEntity>("GetStoreById", new SqlParameter("StoreID", StoreID)).FirstOrDefault();
                objStoreHTMLEntity.logo = objStoreImage.logo;

                var objStoreHTMLStoreSummary = objGenericRepository.ExecuteSQL<StoreSummary>("GetActiveInAciveCustomersByStore", new SqlParameter("StoreID", StoreID)).ToList();
                objStoreHTMLEntity.storeSummary = objStoreHTMLStoreSummary;

                var objPointsRemaining = objGenericRepository.ExecuteSQL<PointsRemaining>("GetRemainingPointsByStore", new SqlParameter("StoreID", StoreID)).ToList();
                objStoreHTMLEntity.pointsRemaining = objPointsRemaining;

                var objParticipantsLoadedByMonth = objGenericRepository.ExecuteSQL<ParticipantsLoadedByMonth>("GetParticipantByMonthByStore", new SqlParameter("StoreID", StoreID),
                                                                                                                                              new SqlParameter("Month", Month),
                                                                                                                                              new SqlParameter("Year", Year)).ToList();
                    
                objStoreHTMLEntity.participantsLoadedByMonth = objParticipantsLoadedByMonth;

                var objPointsloadedbyMonth = objGenericRepository.ExecuteSQL<PointsLoadedByMonth>("GetPointsLoadedByMonthByStore", new SqlParameter("StoreID", StoreID),
                                                                                                                                   new SqlParameter("Month", Month),
                                                                                                                                   new SqlParameter("Year", Year)).ToList(); 
                objStoreHTMLEntity.pointsLoadedByMonth = objPointsloadedbyMonth;

                var objPointsRedeemedByMonth = objGenericRepository.ExecuteSQL<PointsRedeemedByMonth>("GetPointsRedeemedByMonthByStore", new SqlParameter("StoreID", StoreID),
                                                                                                                                         new SqlParameter("Month", Month),
                                                                                                                                         new SqlParameter("Year", Year)).ToList();
                objStoreHTMLEntity.pointsRedeemedByMonth = objPointsRedeemedByMonth;

                var objTopPointsHolders = objGenericRepository.ExecuteSQL<TopPointsHoldersByStore>("GetTOPPointsHoldersByStore", new SqlParameter("StoreID", StoreID)).ToList();
                objStoreHTMLEntity.topPointsHolders = objTopPointsHolders;

                var objOrderPlacedByType = objGenericRepository.ExecuteSQL<OrderPlacedByTypeByStore>("GetOrderPlacedByTypeByStore", new SqlParameter("StoreID", StoreID),
                                                                                                                                  new SqlParameter("Month", Month),
                                                                                                                                  new SqlParameter("Year", Year)).ToList();
                objStoreHTMLEntity.orderPlacedByType = objOrderPlacedByType;

                var objVoucherOrderByType = objGenericRepository.ExecuteSQL<VoucherOrderByType>("GetVoucherOrderByType", new SqlParameter("StoreID", StoreID)).ToList();
                objStoreHTMLEntity.voucherOrderByType = objVoucherOrderByType;

                var objOrdersPlacedByTypeLastYear = objGenericRepository.ExecuteSQL<OrdersPlacedByTypeLastYear>("GetOrdersPlacedByTypeLast12Months", new SqlParameter("StoreID", StoreID)).ToList();
                objStoreHTMLEntity.ordersPlacedByTypeLastYear = objOrdersPlacedByTypeLastYear;

                return objStoreHTMLEntity;
            }
            catch (Exception e)
            {
                throw;
            }
        }


         
    }
}
