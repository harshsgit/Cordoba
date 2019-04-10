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
using System.Configuration;

namespace CordobaServices.Services
{
    public class ProductPurchasedReportService : IProductPurchasedReportServices
    {

        private GenericRepository<ProductEntity> objGenericRepository = new GenericRepository<ProductEntity>();

        public List<OrderStatusEntity> GetOrderStatus(int store_id, int LoggedInUserId, int language_id)
        {
            List<OrderStatusEntity> OrderStatus = new List<OrderStatusEntity>();
            try
            {

                var Result = objGenericRepository.ExecuteSQL<OrderStatusEntity>("GetOrderStatus", new SqlParameter { ParameterName = "language_id", DbType = DbType.Int32, Value = language_id }
                                                                                                , new SqlParameter("StoreId", store_id)
                                                                                                , new SqlParameter("LoggedInUserId", LoggedInUserId)).ToList<OrderStatusEntity>();
                if (Result != null)
                    OrderStatus = Result.ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OrderStatus;
        }


        public List<OrderProductEntity> GetProductPurchasedList(string sortColumn, int order_status_id, int store_id, int LoggedInUserId, TableParameter<OrderProductEntity> filter, DateTime? DateStart, DateTime? DateEnd)
        {
            try
            {
               
                
                var paramOrderBy = new SqlParameter { ParameterName = "OrderBy", DbType = DbType.String, Value = sortColumn };
                var paramPageSize = new SqlParameter { ParameterName = "PageSize", DbType = DbType.Int32, Value = filter != null ? filter.iDisplayLength : 10 };
                var paramPageIndex = new SqlParameter { ParameterName = "PageIndex", DbType = DbType.Int32, Value = filter != null ? filter.PageIndex : 1 };
                var paramOrderStatusId = new SqlParameter { ParameterName = "order_status_id", DbType = DbType.Int32, Value = order_status_id  };
                var paramStoreId = new SqlParameter { ParameterName = "store_id", DbType = DbType.Int32, Value = store_id };
                //var ParameterLoggedInUserId = new SqlParameter
                //{
                //    ParameterName = "LoggedInUserId",
                //    DbType = DbType.Int32,
                //    Value = LoggedInUserId
                //};
                var paramStartDate = new SqlParameter { ParameterName = "DateStart", DbType = DbType.DateTime, Value = DateStart ?? (object)DBNull.Value };
                var paramEndDate = new SqlParameter { ParameterName = "DateEnd", DbType = DbType.DateTime, Value = DateEnd ?? (object)DBNull.Value };
                var query = objGenericRepository.ExecuteSQL<OrderProductEntity>("GetProductPurchasedList", paramOrderBy, paramPageSize, paramPageIndex, paramOrderStatusId, paramStoreId, paramStartDate, paramEndDate).ToList<OrderProductEntity>();
                return query;
            }
            catch (Exception ex )
            {
                throw ex ;
            }
        }

        public List<OrderProductEntity> GetProductViewedList(int store_id, int LoggedInUserId, string sortColumn,  TableParameter<OrderProductEntity> filter)
        {
            try
            {
                var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = store_id
                };
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var paramOrderBy = new SqlParameter { ParameterName = "OrderBy", DbType = DbType.String, Value = sortColumn };
                var paramPageSize = new SqlParameter { ParameterName = "PageSize", DbType = DbType.Int32, Value = filter != null ? filter.iDisplayLength : 10 };
                var paramPageIndex = new SqlParameter { ParameterName = "PageIndex", DbType = DbType.Int32, Value = filter != null ? filter.PageIndex : 1 };
                var query = objGenericRepository.ExecuteSQL<OrderProductEntity>("GetProductViewedList", ParameterStoreId, ParameterLoggedInUserId, paramOrderBy, paramPageSize, paramPageIndex).ToList<OrderProductEntity>();
                return query;
            }
            catch (Exception ex )
            {
                throw ex ;
            }
        }


        public DataSet ExportToExcelProductPurchasedList(string sortColumn, int order_status_id, int store_id, int LoggedInUserId, DateTime? DateStart, DateTime? DateEnd)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                cmd = new SqlCommand("GetProductPurchasedList", con);
                cmd.Parameters.Add(new SqlParameter("@OrderBy", sortColumn));
                cmd.Parameters.Add(new SqlParameter("@PageSize", 1000000));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", 1));
                cmd.Parameters.Add(new SqlParameter("@order_status_id", order_status_id));
                cmd.Parameters.Add(new SqlParameter("@store_id", store_id));
                cmd.Parameters.Add(new SqlParameter("@LoggedInUserId", LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("@DateStart", DateStart ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DateEnd", DateEnd ?? (object)DBNull.Value));
                cmd.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "data");
                return ds;
           }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    cmd.Dispose();
                }
            }
        }
    }
}
