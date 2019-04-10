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
using CordobaServices.Helpers;
using System.Configuration; 

namespace CordobaServices.Services
{
    public class ReportService : IReportService
    {
        private GenericRepository<ReportEntity> ReportEntityGenericRepository = new GenericRepository<ReportEntity>();

        public IEnumerable<ReportEntity> GetReturnList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int? GroupById, int? StatusId, int? StoreId, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                     new SqlParameter("OrderBy", sortColumn!=null ? sortColumn:(object)DBNull.Value)
                    ,new SqlParameter("PageSize",filter != null ? filter.iDisplayLength : 10)
                    ,new SqlParameter("PageIndex",filter != null ? filter.PageIndex : 1)                   
                    ,new SqlParameter("DateStart", DateStart!=null ? DateStart:(object)DBNull.Value)
                    ,new SqlParameter("DateEnd", DateEnd!=null ? DateEnd:(object)DBNull.Value)
                    ,new SqlParameter("GroupById", GroupById!=null ? GroupById:(object)DBNull.Value)
                    ,new SqlParameter("StatusId", StatusId!=null ? StatusId:(object)DBNull.Value)
                    ,new SqlParameter("store_id", StoreId!=null ? StoreId:(object)DBNull.Value)
                    ,new SqlParameter("LoggedInUserId", LoggedInUserId!=null ? LoggedInUserId:(object)DBNull.Value)
                };

                var query = ReportEntityGenericRepository.ExecuteSQL<ReportEntity>("GetSalesReturnReportList", param).AsQueryable();

                return query;
            }
            catch (Exception)
            {
                throw;
            }

            //return result;
        }

        public IEnumerable<ReportEntity> GetOrderReportList(string sortColumn, Nullable<DateTime> DateStart, Nullable<DateTime> DateEnd, int? GroupById, int? StatusId, int? StoreId, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                     new SqlParameter("OrderBy", sortColumn!=null ? sortColumn:(object)DBNull.Value)
                    ,new SqlParameter("PageSize",filter != null ? filter.iDisplayLength : 10)
                    ,new SqlParameter("PageIndex",filter != null ? filter.PageIndex : 1)                   
                    ,new SqlParameter("DateStart", DateStart!=null ? DateStart:(object)DBNull.Value)
                    ,new SqlParameter("DateEnd", DateEnd!=null ? DateEnd:(object)DBNull.Value)
                    ,new SqlParameter("GroupById", GroupById!=null ? GroupById:(object)DBNull.Value)
                    ,new SqlParameter("StatusId", StatusId!=null ? StatusId:(object)DBNull.Value)
                    ,new SqlParameter("store_id", StoreId!=null ? StoreId:(object)DBNull.Value)
                };


                var query = ReportEntityGenericRepository.ExecuteSQL<ReportEntity>("GetOrderReportList", param).AsQueryable();

                return query;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public IEnumerable<ReportEntity> GetTransactionReportList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int store_id, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                     new SqlParameter("OrderBy", sortColumn!=null ? sortColumn:(object)DBNull.Value)
                    ,new SqlParameter("PageSize",filter != null ? filter.iDisplayLength : 10)
                    ,new SqlParameter("PageIndex",filter != null ? filter.PageIndex : 1)                   
                    ,new SqlParameter("DateStart", DateStart!=null ? DateStart:(object)DBNull.Value)
                    ,new SqlParameter("DateEnd", DateEnd!=null ? DateEnd:(object)DBNull.Value)
                    ,new SqlParameter("store_id", store_id!=null ? store_id:(object)DBNull.Value)
                    //,new SqlParameter("LoggedInUserId", LoggedInUserId!=null ? LoggedInUserId:(object)DBNull.Value)
                };


                var query = ReportEntityGenericRepository.ExecuteSQL<ReportEntity>("GetTransectionReportList", param).AsQueryable();

                return query;
            }
            catch (Exception)
            {
                throw;
            }

            //return result;
        }

        public IEnumerable<ReportEntity> GetCustomerBalanceReportList(string sortColumn,string storeids,DateTime? Date, TableParameter<ReportEntity> filter)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                     new SqlParameter("OrderBy", sortColumn!=null ? sortColumn:(object)DBNull.Value)
                    ,new SqlParameter("PageSize",filter != null ? filter.iDisplayLength : 10)
                    ,new SqlParameter("PageIndex",filter != null ? filter.PageIndex : 1)                   
                    ,new SqlParameter("storeids", !string.IsNullOrWhiteSpace(storeids) ? storeids:(object)DBNull.Value)
                    ,new SqlParameter("Date", Date!=null ? Date:(object)DBNull.Value)
                };

                var query = ReportEntityGenericRepository.ExecuteSQL<ReportEntity>("GetCustomerBalanceReportList", param).AsQueryable();
                return query;
            }
            catch (Exception)
            {
                throw;
            }

            //return result;
        }

        public DataSet TransactionReportExportToExcel(string sortColumn, object tableParameter, DateTime? dateStart, DateTime? dateEnd, int? storeId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                cmd = new SqlCommand("GetTransectionReportList", con);
                cmd.Parameters.Add(new SqlParameter("@OrderBy", sortColumn));
                cmd.Parameters.Add(new SqlParameter("@PageSize", 10000000));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", 1));
                cmd.Parameters.Add(new SqlParameter("@DateStart", dateStart ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DateEnd", dateEnd));
                cmd.Parameters.Add(new SqlParameter("@store_id", storeId?? (object)DBNull.Value));
                
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

        public DataSet CustomerBalanceReportExportToExcel(string sortColumn, object tableParameter, string StoreIDs, DateTime? Date)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                cmd = new SqlCommand("GetCustomerBalanceReportList", con);
                cmd.Parameters.Add(new SqlParameter("@OrderBy", sortColumn));
                cmd.Parameters.Add(new SqlParameter("@PageSize", 10000000));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", 1));
                cmd.Parameters.Add(new SqlParameter("@StoreIDs", StoreIDs ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Date", Date?? (object)DBNull.Value));

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

        public IEnumerable<ReportEntity> GetTransactionItemReportList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int store_id, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                     new SqlParameter("OrderBy", sortColumn!=null ? sortColumn:(object)DBNull.Value)
                    ,new SqlParameter("PageSize",filter != null ? filter.iDisplayLength : 10)
                    ,new SqlParameter("PageIndex",filter != null ? filter.PageIndex : 1)                   
                    ,new SqlParameter("DateStart", DateStart!=null ? DateStart:(object)DBNull.Value)
                    ,new SqlParameter("DateEnd", DateEnd!=null ? DateEnd:(object)DBNull.Value)
                    ,new SqlParameter("store_id", store_id!=null ? store_id:(object)DBNull.Value)
                    //,new SqlParameter("LoggedInUserId", LoggedInUserId!=null ? LoggedInUserId:(object)DBNull.Value)
                };


                var query = ReportEntityGenericRepository.ExecuteSQL<ReportEntity>("GetTransactionItemReportList", param).AsQueryable();

                return query;
            }
            catch (Exception)
            {
                throw;
            }

            //return result;
        }


        public DataSet TransactionItemReportExportToExcel(string sortColumn, object tableParameter, DateTime? dateStart, DateTime? dateEnd, int? storeId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                cmd = new SqlCommand("GetTransactionItemReportList", con);
                cmd.Parameters.Add(new SqlParameter("@OrderBy", sortColumn));
                cmd.Parameters.Add(new SqlParameter("@PageSize", 10000000));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", 1));
                cmd.Parameters.Add(new SqlParameter("@DateStart", dateStart ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DateEnd", dateEnd));
                cmd.Parameters.Add(new SqlParameter("@store_id", storeId ?? (object)DBNull.Value));

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

        public IEnumerable<ReportEntity> GetTransactionItemCategoryReportList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int store_id, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                     new SqlParameter("OrderBy", sortColumn!=null ? sortColumn:(object)DBNull.Value)
                    ,new SqlParameter("PageSize",filter != null ? filter.iDisplayLength : 10)
                    ,new SqlParameter("PageIndex",filter != null ? filter.PageIndex : 1)                   
                    ,new SqlParameter("DateStart", DateStart!=null ? DateStart:(object)DBNull.Value)
                    ,new SqlParameter("DateEnd", DateEnd!=null ? DateEnd:(object)DBNull.Value)
                    ,new SqlParameter("store_id", store_id!=null ? store_id:(object)DBNull.Value)
                    //,new SqlParameter("LoggedInUserId", LoggedInUserId!=null ? LoggedInUserId:(object)DBNull.Value)
                };


                var query = ReportEntityGenericRepository.ExecuteSQL<ReportEntity>("GetTransactionItemCategoryReportList", param).AsQueryable();

                return query;
            }
            catch (Exception)
            {
                throw;
            }

            //return result;
        }

        public DataSet TransactionItemCategoryReportExportToExcel(string sortColumn, object tableParameter, DateTime? dateStart, DateTime? dateEnd, int? storeId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                cmd = new SqlCommand("GetTransactionItemCategoryReportList", con);
                cmd.Parameters.Add(new SqlParameter("@OrderBy", sortColumn));
                cmd.Parameters.Add(new SqlParameter("@PageSize", 10000000));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", 1));
                cmd.Parameters.Add(new SqlParameter("@DateStart", dateStart ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DateEnd", dateEnd));
                cmd.Parameters.Add(new SqlParameter("@store_id", storeId ?? (object)DBNull.Value));

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

        public IEnumerable<ReportEntity> GetStoreReportList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int store_id, TableParameter<ReportEntity> filter, string PageFrom = "")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                     new SqlParameter("OrderBy", sortColumn!=null ? sortColumn:(object)DBNull.Value)
                    ,new SqlParameter("PageSize",filter != null ? filter.iDisplayLength : 10)
                    ,new SqlParameter("PageIndex",filter != null ? filter.PageIndex : 1)                   
                    ,new SqlParameter("DateStart", DateStart!=null ? DateStart:(object)DBNull.Value)
                    ,new SqlParameter("DateEnd", DateEnd!=null ? DateEnd:(object)DBNull.Value)
                    ,new SqlParameter("store_id", store_id )
                    //,new SqlParameter("LoggedInUserId", LoggedInUserId!=null ? LoggedInUserId:(object)DBNull.Value)
                };


                var query = ReportEntityGenericRepository.ExecuteSQL<ReportEntity>("GetStoreReportList", param).AsQueryable();

                return query;
            }
            catch (Exception)
            {
                throw;
            }

            //return result;
        }

        public List<StoreChartEntity> GetCustomerByStoreForChart(int store_id, int ChartFilterType)
        {
            List<StoreChartEntity> customerByStoreForChart = new List<StoreChartEntity>();
            try
            {
                var paramStoreId = new SqlParameter
                {
                    ParameterName = "store_id",
                    DbType = DbType.Int32,
                    Value = store_id
                };

                var paramChartFilterType = new SqlParameter
                {
                    ParameterName = "ChartFilterType",
                    DbType = DbType.Int32,
                    Value = ChartFilterType
                };
                customerByStoreForChart = ReportEntityGenericRepository.ExecuteSQL<StoreChartEntity>("GetCustomerByStoreForChart", paramStoreId, paramChartFilterType).ToList<StoreChartEntity>().ToList();
                return customerByStoreForChart;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
