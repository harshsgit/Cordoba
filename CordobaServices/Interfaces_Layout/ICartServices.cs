using CordobaModels;
using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces_Layout
{
  public interface ICartServices
    {
      List<CartEntity> GetCartDetailsByCustomerAndStoreId(int? StoreID, int CustomerId);
      List<CartEntity> GetCartDetailsByCartGroupId(int? StoreID, int cartgroup_id);
      int? RemoveProductFromCart(int? CartId);

      int? PlaceOrder(PlaceOrderEntity placeOrderObj);
      List<AddressEntity> GetCustmoreAddressList(int? store_id, int customer_id);

    }
}
