using CordobaCommon.Enum;
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
    public class DashboardService : IDashboardService
    {       
        private GenericRepository<OrderEntity> objGenericRepository = new GenericRepository<OrderEntity>();

        public List<OrderEntity> GetLatestOrderDetailsDashboard(int storeId)
        {           
            List<OrderEntity> orders = new List<OrderEntity>();
            var paramStoreId = new SqlParameter { ParameterName = "store_id", DbType = DbType.Int32, Value = storeId };
            orders = objGenericRepository.ExecuteSQL<OrderEntity>("GetLatestOrderDetailsDashboard", paramStoreId).ToList();
            return orders;
        }

        public DashboardSummaryEntity GetDashboardTopHeaderFields(int storeId)
        {
            var paramStoreId = new SqlParameter { ParameterName = "storeId", DbType = DbType.Int32, Value = storeId };
            var dashboardHeaderFields = objGenericRepository.ExecuteSQL<DashboardSummaryEntity>("GetDashboardTopHeaderFields", paramStoreId).SingleOrDefault();
            return dashboardHeaderFields;
        }

        public DashboardSummaryEntity GetDashboardSummaryCharts(int storeId, int ChartFiltertype, int ChartOrFunctionTypeEnum)
        {
           
            DashboardSummaryEntity objDashboardSummaryEntity = new DashboardSummaryEntity();
            switch (ChartOrFunctionTypeEnum)
            {
                     
                case 0:
                          var objDashboardOrderSummary = objGenericRepository.ExecuteSQL<DashboardOrderSummary>("GetDashboardOrderSummary_Chart", new SqlParameter("storeId", storeId)).ToList();

                          var objDashboardSalesAnalytics = objGenericRepository.ExecuteSQL<DashboardSalesAnalytics>("GetDashboardSalesAnalytics_Chart", new SqlParameter("storeId", storeId), new SqlParameter("ChartFiltertype", ChartFiltertype)).ToList();

                          var objDashboardTopSellStore = objGenericRepository.ExecuteSQL<DashboardTopSellStore>("GetDashboardTop5_SellStore_Chart", new SqlParameter("storeId", storeId)).ToList();

                          var objDashboardTopPurchaseProduct = objGenericRepository.ExecuteSQL<DashboardTopPurchaseProduct>("GetDashboardTop5_PurchaseProduct_Chart", new SqlParameter("storeId", storeId)).ToList();

                          var objDashboardTopCustomer = objGenericRepository.ExecuteSQL<DashboardTopCustomer>("GetDashboardTop5_Customers_Chart", new SqlParameter("storeId", storeId)).ToList();

                          objDashboardSummaryEntity.dashboardOrderSummary = objDashboardOrderSummary;
                          objDashboardSummaryEntity.dashboardSalesAnalytics = objDashboardSalesAnalytics;
                          objDashboardSummaryEntity.dashboardTopSellStore = objDashboardTopSellStore;
                          objDashboardSummaryEntity.dashboardTopPurchaseProduct = objDashboardTopPurchaseProduct;
                          objDashboardSummaryEntity.dashboardTopCustomer = objDashboardTopCustomer;
                          break;
                case 1:
                          var objDashboardOrderSummaryList = objGenericRepository.ExecuteSQL<DashboardOrderSummary>("GetDashboardOrderSummary_Chart", new SqlParameter("storeId", storeId)).ToList();
                          objDashboardSummaryEntity.dashboardOrderSummary = objDashboardOrderSummaryList;
                          break;
                case 2:
                          var objDashboardSalesAnalyticsList = objGenericRepository.ExecuteSQL<DashboardSalesAnalytics>("GetDashboardSalesAnalytics_Chart", new SqlParameter("storeId", storeId), new SqlParameter("ChartFiltertype", ChartOrFunctionTypeEnum)).ToList();
                          objDashboardSummaryEntity.dashboardSalesAnalytics = objDashboardSalesAnalyticsList;
                          break;
                case 3:
                          var objDashboardTopSellStoreList = objGenericRepository.ExecuteSQL<DashboardTopSellStore>("GetDashboardTop5_SellStore_Chart", new SqlParameter("storeId", storeId)).ToList();
                          objDashboardSummaryEntity.dashboardTopSellStore = objDashboardTopSellStoreList;
                          break;
                case 4:
                          var objDashboardTopCustomerList = objGenericRepository.ExecuteSQL<DashboardTopCustomer>("GetDashboardTop5_Customers_Chart", new SqlParameter("storeId", storeId)).ToList();
                          objDashboardSummaryEntity.dashboardTopCustomer = objDashboardTopCustomerList;
                          break;
                case 5:
                          var objDashboardTopPurchaseProductList = objGenericRepository.ExecuteSQL<DashboardTopPurchaseProduct>("GetDashboardTop5_PurchaseProduct_Chart", new SqlParameter("storeId", storeId)).ToList();
                          objDashboardSummaryEntity.dashboardTopPurchaseProduct = objDashboardTopPurchaseProductList;
                          break;

            }

           
            return objDashboardSummaryEntity;
        }
    }
}
