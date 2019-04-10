using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CordobaServices.Helpers;

namespace CordobaServices.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderEntity> GetOrderList(int StoreId, int LoggedInUserId, string sortColumn, int? orderId, int? order_status_id, string CustomerName, Nullable<DateTime> DateAdded, Nullable<DateTime> DateModified, TableParameter<OrderEntity> filter, string PageFrom = "");

        List<OrderEntity> GetOrderDetails(int StoreId, int LoggedInUserId, int orderId);

        List<OrderEntity> GetOrderHistory(int StoreId, int LoggedInUserId, int customer_Id);

        int InsertOrderHistory(int StoreId, int LoggedInUserId, OrderHistoryEntity objHistoryEntity);

        List<CustomerGroupEntity> GetCustomerGroupList(int StoreId, int LoggedInUserId);

        List<CurrencyEntity> GetCurrencyList(int StoreId, int LoggedInUserId);

        List<ZoneEntity> GetZoneListByCountry(int StoreId, int LoggedInUserId, int countryId);

        List<AddressEntity> GetCustomerAddress(int StoreId, int LoggedInUserId, int orderId);

        int UpdateOrder_CutomerDetails(int LoggedInUserId, OrderEntity objOrderEntity);

        int UpdateOrder_PaymentDetails(int StoreId, int LoggedInUserId, OrderEntity objOrderEntity);

        int UpdateOrder_ShippingDetails(int StoreId, int LoggedInUserId, OrderEntity objOrderEntity);

        List<CustomerEntity> GetCustomersByStore(int storeId, int LoggedInUserId);

        int UpdateOrder_TotalDetails(int StoreId, int LoggedInUserId, int order_id, int order_status_id, string comment);

        OrderEntity GetOrderDetail_Layout(int order_id, int store_id);

        int UpdateOrderStatus(int OrderId, int OrderStatusId, string Comment);

        int UpdateOrderDate(int OrderId, DateTime OrderDate);
    }
}
