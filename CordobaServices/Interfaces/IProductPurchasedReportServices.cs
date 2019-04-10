using CordobaModels.Entities;
using CordobaServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
   public interface IProductPurchasedReportServices
    {
       List<OrderStatusEntity> GetOrderStatus(int store_id, int LoggedInUserId, int language_id);

       List<OrderProductEntity> GetProductPurchasedList(string sortColumn, int order_status_id, int store_id, int LoggedInUserId, TableParameter<OrderProductEntity> tableParameter, DateTime? DateStart, DateTime? DateEnd);

       List<OrderProductEntity> GetProductViewedList(int store_id, int LoggedInUserId, string sortColumn, TableParameter<OrderProductEntity> tableParameter);

       DataSet ExportToExcelProductPurchasedList(string sortColumn, int order_status_id, int store_id, int LoggedInUserId, DateTime? DateStart, DateTime? DateEnd);

   }
}
