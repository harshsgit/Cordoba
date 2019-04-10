using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class DashboardSummaryEntity
    {
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public decimal? CreditBalance { get; set; }
        public decimal? AssignedBalance { get; set; }
        public decimal? TotalSales { get; set; }
        public decimal? OutOfStockProduct { get; set; }
        public int TotalSuppliers { get; set; }

        public List<DashboardOrderSummary> dashboardOrderSummary { get; set; }

        public List<DashboardSalesAnalytics> dashboardSalesAnalytics { get; set; }

        public List<DashboardTopSellStore> dashboardTopSellStore { get; set; }

        public List<DashboardTopPurchaseProduct> dashboardTopPurchaseProduct { get; set; }

        public List<DashboardTopCustomer> dashboardTopCustomer { get; set; }
    }

    public class DashboardOrderSummary
    {
        public string Month { get; set; }
        public int? orderCount { get; set; }
    }

    public class DashboardSalesAnalytics
    {
        public int? OrderCount { get; set; }
        public int? CustomerCount { get; set; }
        public string WeekDayName { get; set; }
        public int? DayNumberOfMonth { get; set; }
        public string MonthName { get; set; }
        public int? HourOfDay { get; set; }
        //public decimal value { get; set; }
    }

    public class DashboardTopSellStore
    {
        public int value { get; set; }
        public string store { get; set; }
    }

    public class DashboardTopPurchaseProduct
    {
        public int value { get; set; }
        public string product { get; set; }
    }

    public class DashboardTopCustomer
    {
        public int value { get; set; }
        public string customer { get; set; }
    }
}
