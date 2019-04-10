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
using System.Web;
using System.IO;

namespace CordobaServices.Services
{
    public class OrderService : IOrderService
    {
        private GenericRepository<OrderEntity> objGenericRepository = new GenericRepository<OrderEntity>();
        private GenericRepository<PlaceOrderEntity> placeOrderRepository = new GenericRepository<PlaceOrderEntity>();

        public List<OrderEntity> GetOrderDetails(int StoreId, int LoggedInUserId, int orderId)
        {
            List<OrderEntity> orders = new List<OrderEntity>();

            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = orderId };
            orders = objGenericRepository.ExecuteSQL<OrderEntity>("GetOrderDetails", paramOrderId).ToList();

            if (orders != null && orders.Count != 0)
            {
                List<OrderProductEntity> orderProducts = new List<OrderProductEntity>();
                var paramOrderProductOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = orderId };
                orderProducts = objGenericRepository.ExecuteSQL<OrderProductEntity>("GetOrderProductByOrderId", paramOrderProductOrderId).ToList();

                List<OrderHistoryEntity> orderHistory = new List<OrderHistoryEntity>();
                var paramOrderHistoryOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = orderId };
                orderHistory = objGenericRepository.ExecuteSQL<OrderHistoryEntity>("GetOrderHistoryByOrderId", paramOrderHistoryOrderId).ToList();

                orders[0].orderProductEntity = orderProducts;
                orders[0].orderHistoryEntity = orderHistory;
            }

            return orders;
        }

        public int InsertOrderHistory(int StoreId, int LoggedInUserId, OrderHistoryEntity objHistoryEntity)
        {

            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = objHistoryEntity.order_id };
            var paramnotify = new SqlParameter { ParameterName = "notify", DbType = DbType.Int32, Value = objHistoryEntity.notify };
            var paramcomment = new SqlParameter { ParameterName = "comment", DbType = DbType.String, Value = objHistoryEntity.comment };
            var paramorderStatusId = new SqlParameter { ParameterName = "order_status_id", DbType = DbType.Int32, Value = objHistoryEntity.order_status_id };
            int result = objGenericRepository.ExecuteSQL<int>("InsertOrderHistory", paramOrderId, paramnotify, paramcomment, paramorderStatusId).FirstOrDefault();
            return result;
        }

        public IEnumerable<OrderEntity> GetOrderList(int StoreId, int LoggedInUserId, string sortColumn, int? orderId, int? order_status_id, string CustomerName, Nullable<DateTime> DateAdded, Nullable<DateTime> DateModified, TableParameter<OrderEntity> filter, string PageFrom = "")
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                new SqlParameter("StoreId",StoreId)
               ,new SqlParameter("LoggedInUserId",LoggedInUserId)
               ,new SqlParameter("OrderBy",sortColumn)
               ,new SqlParameter("PageSize",filter != null ? filter.iDisplayLength : 10 )
               ,new SqlParameter("PageIndex", filter != null ? filter.PageIndex : 1)
               ,new SqlParameter("PageFrom",PageFrom)
               ,new SqlParameter("orderId ",orderId!=null?orderId:0)
               ,new SqlParameter("order_status_id ",order_status_id!=null?order_status_id:0)
               ,new SqlParameter("customer ",CustomerName!=null?CustomerName:(object)DBNull.Value) 
               //,new SqlParameter("total ",total!=null?total:0)
               ,new SqlParameter("dateAdded ",DateAdded!=null?DateAdded:(object)DBNull.Value)
               ,new SqlParameter("dateModifies ",DateModified!=null?DateModified:(object)DBNull.Value)
                };

                var query = objGenericRepository.ExecuteSQL<OrderEntity>("GetOrderList_Admin", sqlParameter).AsQueryable();
                return query;
            }
            catch (Exception)
            {
                throw;
            }

            //return result;
        }

        public List<OrderEntity> GetOrderHistory(int StoreId, int LoggedInUserId, int CustomerId)
        {
            try
            {

                var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var paramCustomerId = new SqlParameter { ParameterName = "customer_id", DbType = DbType.Int32, Value = CustomerId };
                var query = objGenericRepository.ExecuteSQL<OrderEntity>("GetOrderHistory", paramCustomerId, ParameterStoreId, ParameterLoggedInUserId).ToList();
                return query;
            }
            catch (Exception)
            {
                throw;
            }

            //return result;
        }


        public List<CustomerGroupEntity> GetCustomerGroupList(int StoreId, int LoggedInUserId)
        {
            try
            {
                var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var listCustomerGroup = objGenericRepository.ExecuteSQL<CustomerGroupEntity>("GetCustomerGroupList", ParameterStoreId, ParameterLoggedInUserId).ToList();
                return listCustomerGroup;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CurrencyEntity> GetCurrencyList(int StoreId, int LoggedInUserId)
        {
            try
            {
                var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var listCustomerGroup = objGenericRepository.ExecuteSQL<CurrencyEntity>("GetCurrencyList", ParameterStoreId, ParameterLoggedInUserId).ToList();
                return listCustomerGroup;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ZoneEntity> GetZoneListByCountry(int StoreId, int LoggedInUserId, int countryId)
        {
            try
            {
                var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var paramCountryid = new SqlParameter { ParameterName = "country_id", DbType = DbType.Int32, Value = countryId };
                var listZones = objGenericRepository.ExecuteSQL<ZoneEntity>("GetZoneListByCountry", ParameterStoreId, ParameterLoggedInUserId, paramCountryid).ToList();
                return listZones;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AddressEntity> GetCustomerAddress(int StoreId, int LoggedInUserId, int orderId)
        {
            try
            {
                var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var paramOrderId = new SqlParameter { ParameterName = "orderId", DbType = DbType.Int32, Value = orderId };
                var listZones = objGenericRepository.ExecuteSQL<AddressEntity>("GetCustomerAddress", ParameterStoreId, ParameterLoggedInUserId, paramOrderId).ToList();
                return listZones;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int UpdateOrder_CutomerDetails(int LoggedInUserId, OrderEntity objOrderEntity)
        {
            try
            {
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var paramstore_id = new SqlParameter { ParameterName = "store_id ", DbType = DbType.Int32, Value = objOrderEntity.store_id };
                var paramcurrency_id = new SqlParameter { ParameterName = "currency_id", DbType = DbType.Int32, Value = objOrderEntity.currency_id };
                var paramcustomer_id = new SqlParameter { ParameterName = "customer_id", DbType = DbType.Int32, Value = objOrderEntity.customer_id };
                var paramcustomer_group_id = new SqlParameter { ParameterName = "customer_group_id", DbType = DbType.Int32, Value = objOrderEntity.customer_group_id };
                var paramfirstname = new SqlParameter { ParameterName = "firstname", DbType = DbType.String, Value = objOrderEntity.firstname };
                var paramlastname = new SqlParameter { ParameterName = "lastname", DbType = DbType.String, Value = objOrderEntity.lastname };
                var paramemail = new SqlParameter { ParameterName = "email", DbType = DbType.String, Value = objOrderEntity.email };
                var paramtelephone = new SqlParameter { ParameterName = "telephone", DbType = DbType.String, Value = objOrderEntity.telephone };
                var paramfax = new SqlParameter { ParameterName = "fax", DbType = DbType.String, Value = objOrderEntity.fax };
                var paramorder_id = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = objOrderEntity.order_id };
                var paramdate_added = new SqlParameter { ParameterName = "date_added", DbType = DbType.DateTime, Value = objOrderEntity.date_added == null ? (object)DBNull.Value : objOrderEntity.date_added };
                var list = objGenericRepository.ExecuteSQL<int>("UpdateOrder_CutomerDetails", ParameterLoggedInUserId, paramstore_id, paramcurrency_id, paramcustomer_id, paramcustomer_group_id,
                    paramfirstname, paramlastname, paramemail,
                    paramtelephone, paramfax, paramorder_id).FirstOrDefault();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateOrder_PaymentDetails(int StoreId, int LoggedInUserId, OrderEntity objOrderEntity)
        {
            try
            {
                var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var paramorder_id = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = objOrderEntity.order_id };
                var paramaddress_id = new SqlParameter { ParameterName = "address_id", DbType = DbType.Int32, Value = objOrderEntity.address_id };
                var parampayment_firstname = new SqlParameter { ParameterName = "payment_firstname", DbType = DbType.String, Value = objOrderEntity.payment_firstname };
                var parampayment_lastname = new SqlParameter { ParameterName = "payment_lastname", DbType = DbType.String, Value = objOrderEntity.payment_lastname };
                var parampayment_company = new SqlParameter { ParameterName = "payment_company", DbType = DbType.String, Value = objOrderEntity.payment_company };
                var parampayment_address_1 = new SqlParameter { ParameterName = "payment_address_1", DbType = DbType.String, Value = objOrderEntity.payment_address_1 };
                var parampayment_address_2 = new SqlParameter { ParameterName = "payment_address_2", DbType = DbType.String, Value = objOrderEntity.payment_address_2 };
                var parampayment_city = new SqlParameter { ParameterName = "payment_city", DbType = DbType.String, Value = objOrderEntity.payment_city };
                var parampayment_postcode = new SqlParameter { ParameterName = "payment_postcode", DbType = DbType.String, Value = objOrderEntity.payment_postcode };
                var parampayment_country = new SqlParameter { ParameterName = "payment_country", DbType = DbType.String, Value = objOrderEntity.payment_country };
                var parampayment_country_id = new SqlParameter { ParameterName = "payment_country_id", DbType = DbType.Int32, Value = objOrderEntity.payment_country_id };
                var parampayment_zone_id = new SqlParameter { ParameterName = "payment_zone_id", DbType = DbType.Int32, Value = objOrderEntity.payment_zone_id };
                var parampayment_zone = new SqlParameter { ParameterName = "payment_zone", DbType = DbType.String, Value = objOrderEntity.payment_zone ?? (object)DBNull.Value };

                var list = objGenericRepository.ExecuteSQL<int>("UpdateOrder_PaymentDetails", ParameterStoreId, ParameterLoggedInUserId, paramorder_id, paramaddress_id, parampayment_firstname, parampayment_lastname, parampayment_company, parampayment_address_1, parampayment_address_2, parampayment_city, parampayment_postcode, parampayment_country, parampayment_country_id, parampayment_zone_id, parampayment_zone).FirstOrDefault();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateOrder_ShippingDetails(int StoreId, int LoggedInUserId, OrderEntity objOrderEntity)
        {
            try
            {
                var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var paramorder_id = new SqlParameter { ParameterName = "order_id ", DbType = DbType.Int32, Value = objOrderEntity.order_id };
                var paramaddress_id = new SqlParameter { ParameterName = "address_id", DbType = DbType.Int32, Value = objOrderEntity.address_id };
                var parampayment_firstname = new SqlParameter { ParameterName = "shipping_firstname", DbType = DbType.String, Value = objOrderEntity.shipping_firstname };
                var parampayment_lastname = new SqlParameter { ParameterName = "shipping_lastname", DbType = DbType.String, Value = objOrderEntity.shipping_lastname };
                var parampayment_company = new SqlParameter { ParameterName = "shipping_company", DbType = DbType.String, Value = objOrderEntity.shipping_company };
                var parampayment_address_1 = new SqlParameter { ParameterName = "shipping_address_1", DbType = DbType.String, Value = objOrderEntity.shipping_address_1 };
                var parampayment_address_2 = new SqlParameter { ParameterName = "shipping_address_2", DbType = DbType.String, Value = objOrderEntity.shipping_address_2 };
                var parampayment_city = new SqlParameter { ParameterName = "shipping_city", DbType = DbType.String, Value = objOrderEntity.shipping_city };
                var parampayment_postcode = new SqlParameter { ParameterName = "shipping_postcode", DbType = DbType.String, Value = objOrderEntity.shipping_postcode };
                var parampayment_country = new SqlParameter { ParameterName = "shipping_country", DbType = DbType.String, Value = objOrderEntity.shipping_country };
                var parampayment_country_id = new SqlParameter { ParameterName = "shipping_country_id", DbType = DbType.Int32, Value = objOrderEntity.shipping_country_id };

                var list = objGenericRepository.ExecuteSQL<int>("UpdateOrder_ShippingDetails", ParameterStoreId, ParameterLoggedInUserId, paramorder_id, paramaddress_id,
                    parampayment_firstname, parampayment_lastname, parampayment_company, parampayment_address_1, parampayment_address_2,
                    parampayment_city, parampayment_postcode, parampayment_country, parampayment_country_id).FirstOrDefault();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CustomerEntity> GetCustomersByStore(int storeId, int LoggedInUserId)
        {
            try
            {
                var paramStore_id = new SqlParameter { ParameterName = "store_id", DbType = DbType.Int32, Value = storeId };
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var list = objGenericRepository.ExecuteSQL<CustomerEntity>("GetCustomersByStore", paramStore_id, ParameterLoggedInUserId).ToList();
                return list;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateOrder_TotalDetails(int StoreId, int LoggedInUserId, int order_id, int order_status_id, string comment)
        {
            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramOrderStatusId = new SqlParameter { ParameterName = "order_status_id", DbType = DbType.Int32, Value = order_status_id };
            var paramorderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = order_id };
            var paramComment = new SqlParameter { ParameterName = "comment", DbType = DbType.String, Value = (comment == null || comment == "") ? " " : comment };
            var result = objGenericRepository.ExecuteSQL<int>("UpdateOrder_TotalDetails", ParameterStoreId, ParameterLoggedInUserId, paramOrderStatusId, paramorderId, paramComment).FirstOrDefault();
            return result;
        }

        public OrderEntity GetOrderDetail_Layout(int order_id, int store_id)
        {
            var OrderEntity = objGenericRepository.ExecuteSQL<OrderEntity>("GetOrderDetail_Layout", new SqlParameter("storeId", store_id), new SqlParameter("order_id", order_id)).FirstOrDefault();

            OrderEntity.orderProductEntity = objGenericRepository.ExecuteSQL<OrderProductEntity>("GetOrderProductDetail_Layout", new SqlParameter("storeId", store_id), new SqlParameter("order_id", order_id)).ToList();
            return OrderEntity;
        }

        public int UpdateOrderStatus(int OrderId, int OrderStatusId, string Comment)
        {
            try
            {
                var sqlParameter = new SqlParameter[]{
                    new SqlParameter("OrderId", OrderId),
                    new SqlParameter("OrderStatusId", OrderStatusId),
                    new SqlParameter("Comment", !string.IsNullOrWhiteSpace(Comment)? Comment : string.Empty)
                };

                var result = objGenericRepository.ExecuteSQL<int>("UpdateOrderStatusDetail", sqlParameter).FirstOrDefault();
                if (result > 0)
                {
                    SendOrderUpdateMailToCustomer(OrderId);
                }
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateOrderDate(int OrderId, DateTime OrderDate)
        {
            try
            {
                var sqlParameter = new SqlParameter[]{
                    new SqlParameter("OrderId", OrderId),
                    new SqlParameter("OrderDate", OrderDate)
                };

                var result = objGenericRepository.ExecuteSQL<int>("UpdateOrderDate", sqlParameter).FirstOrDefault();
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }



        public bool SendOrderUpdateMailToCustomer(int order_id)
        {

            var orderIdParam = new SqlParameter
            {
                ParameterName = "order_id",
                DbType = DbType.Int64,
                Value = order_id
            };

            var result = placeOrderRepository.ExecuteSQL<PlaceOrderEntity>("GetOrderDetailsForEmail", orderIdParam);

            var listOrderDetailsresponse = result.ToList();
            var orderItemDetailsRecord = listOrderDetailsresponse.FirstOrDefault();



            #region Generate Email body

            var priceTableString = string.Empty;
            priceTableString = priceTableString +
            @"<table cellspacing='0' cellpadding='0' style='width:100%;font-family:Arial,Helvetica,sans-serif'>
                              <tr style='font-weight:bold;height:25px;'>
                                    <td style='text-align:left;border-bottom: 1px solid #ddd;width:60%;'>
                                              Item
                                    </td>
                                    <td style='text-align:left;border-bottom: 1px solid #ddd;width:25%;'>
                                              Model
                                    </td>
                                    <td style='text-align:center;border-bottom: 1px solid #ddd;width:5%;'>
                                              Quantity
                                    </td>
                                    <td style='text-align:right;border-bottom: 1px solid #ddd;width:10%' >
                                              Price
                                    </td>
                              </tr>";
            priceTableString = listOrderDetailsresponse.Aggregate(priceTableString, (current, a) => current + @"<tr style='font-weight:normal;height:25px;'>
                                     <td  style='text-align: left;'>" + a.product_name + "</td><td style='text-align:left;'>" + a.model + "</td><td style='text-align:center;'>" + a.quantity + "</td><td style='text-align:right;'>" + a.product_price + "</td></tr>");


            priceTableString = priceTableString +
               // ReSharper disable once PossibleNullReferenceException
               @"<tr style='font-weight:bold;height:25px;'>
                                     <td   style='text-align:left;border-top: 1px solid #ddd;' colspan='3'>Total</td><td style='text-align:right;border-top: 1px solid #ddd;'>" + (orderItemDetailsRecord != null ? orderItemDetailsRecord.total.ToString() : "") + "</td></tr>";

            priceTableString = priceTableString + "<table>";

            #endregion

            var filepath = HttpContext.Current.Server.MapPath("~/EmailTemplate/OrderStatusUpdateTemplate.html");

            string strSubject = orderItemDetailsRecord.store_name + " -  Order " + Convert.ToString(orderItemDetailsRecord.order_id);
            var strbody = ReadTextFile(filepath);
            if (strbody.Length > 0)
            {
                strbody = strbody.Replace("##OrderId##", Convert.ToString(orderItemDetailsRecord.order_id));//
                strbody = strbody.Replace("##InvoiceId##", Convert.ToString(orderItemDetailsRecord.invoice_id));
                strbody = strbody.Replace("##StoreName##", orderItemDetailsRecord.store_name);//
                strbody = strbody.Replace("##CustumerName##", orderItemDetailsRecord.customer_name);
                strbody = strbody.Replace("##CustumerPhone##", orderItemDetailsRecord.telephone);
                strbody = strbody.Replace("##CustumerEmail##", orderItemDetailsRecord.email);
                strbody = strbody.Replace("##OrderStatusName##", orderItemDetailsRecord.OrderStatusName);
                strbody = strbody.Replace("##OrderDate##", orderItemDetailsRecord.OrderDate.ToString(Common.DateFormate));
                strbody = strbody.Replace("##PaymentName##", orderItemDetailsRecord.payment_name);
                strbody = strbody.Replace("##PaymentAddress##", orderItemDetailsRecord.payment_address);
                strbody = strbody.Replace("##PaymentMethod##", orderItemDetailsRecord.payment_method);
                strbody = strbody.Replace("##ShippingAddress##", orderItemDetailsRecord.shipping_address);
                strbody = strbody.Replace("##ShippingName##", orderItemDetailsRecord.shipping_name);
                strbody = strbody.Replace("##ShippingCompany##", orderItemDetailsRecord.shipping_company);
                strbody = strbody.Replace("##ShippingMethod##", orderItemDetailsRecord.shipping_method);
                //strbody = strbody.Replace("##Currency##", orderItemDetailsRecord.currencyTitle);
                strbody = strbody.Replace("##FinalTable##", priceTableString);
                //strbody = strbody.Replace("##RedirectPath##", redirectPath);

                //strbody = strbody.Replace("##LeaveFeedback##", leaveFeedbackUrl);
                strbody = strbody.Replace("##StoreLogo##", orderItemDetailsRecord.store_logo);
            }

            var commonServices = new CommonService();

            CommonService.SendMailMessage(orderItemDetailsRecord.email, null, null, strSubject, strbody, commonServices.GetEmailSettings(), null, orderItemDetailsRecord.store_name);
            return true;
        }



        // READ TEXT FILE
        public static string ReadTextFile(string strFilePath)
        {
            var entireFile = string.Empty;
            StreamReader objectRead = null;

            try
            {
                ////open text file
                objectRead = File.OpenText(strFilePath);
                entireFile = objectRead.ReadToEnd();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                Security.DisposeOf(objectRead);
            }

            return entireFile;
        }




    }
}
