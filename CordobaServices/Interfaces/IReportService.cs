using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CordobaServices.Helpers;
using System.Data;

namespace CordobaServices.Interfaces
{
    public interface IReportService
    {
        //Return Report
        //IEnumerable<ReportEntity> GetReturnList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int? GroupById, int? StatusId, TableParameter<ReportEntity> filter, string PageFrom = "");

        IEnumerable<ReportEntity> GetReturnList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int? GroupById, int? StatusId, int? StoreId, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "");
        //Sales order Report
        IEnumerable<ReportEntity> GetOrderReportList(string sortColumn, Nullable<DateTime> DateStart, Nullable<DateTime> DateEnd, int? GroupById, int? StatusId, int? StoreId, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "");
        
        //Transaction Report
        IEnumerable<ReportEntity> GetTransactionReportList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int StoreId, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "");

        DataSet TransactionReportExportToExcel(string sortColumn, object tableParameter, DateTime? dateStart, DateTime? dateEnd, int? storeId);

        //Customer Balance Report 
        IEnumerable<ReportEntity> GetCustomerBalanceReportList(string sortColumn, string storeids, DateTime? Date, TableParameter<ReportEntity> filter);

        DataSet CustomerBalanceReportExportToExcel(string sortColumn, object tableParameter, string StoreIDs, DateTime? Date);

        //Transaction Item Report
        IEnumerable<ReportEntity> GetTransactionItemReportList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int StoreId, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "");

        DataSet TransactionItemReportExportToExcel(string sortColumn, object tableParameter, DateTime? dateStart, DateTime? dateEnd, int? storeId);

        //Transaction Item Category Report
        IEnumerable<ReportEntity> GetTransactionItemCategoryReportList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int StoreId, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "");

        DataSet TransactionItemCategoryReportExportToExcel(string sortColumn, object tableParameter, DateTime? dateStart, DateTime? dateEnd, int? storeId);

        IEnumerable<ReportEntity> GetStoreReportList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int StoreId, TableParameter<ReportEntity> filter, string PageFrom = "");

        List<StoreChartEntity> GetCustomerByStoreForChart(int store_id, int ChartFilterType);
    
    }
}
