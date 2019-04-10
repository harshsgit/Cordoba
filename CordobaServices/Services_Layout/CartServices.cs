using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces_Layout;
using System;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.Mail;
using CordobaServices.Services;

namespace CordobaServices.Services_Layout
{
    public class CartServices : ICartServices
    {
        private GenericRepository<CartEntity> objGenericRepository = new GenericRepository<CartEntity>();
        private GenericRepository<PlaceOrderEntity> placeOrderRepository = new GenericRepository<PlaceOrderEntity>();


        public List<CartEntity> GetCartDetailsByCustomerAndStoreId(int? StoreID, int CustomerId)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("StoreID", StoreID)
                                                 , new SqlParameter("CustomerId",CustomerId)
                                               };

            var result = objGenericRepository.ExecuteSQL<CartEntity>("GetCartDetailsByCustomerAndStoreId", sqlParameter).ToList();
            return result;

        }

        public List<CartEntity> GetCartDetailsByCartGroupId(int? StoreID, int cartgroup_id)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("StoreID", StoreID)
                                                 , new SqlParameter("cartgroup_id",cartgroup_id)
                                               };

            var result = objGenericRepository.ExecuteSQL<CartEntity>("GetCartDetailsByCartGroupId", sqlParameter).ToList();
            return result;

        }

        public int? RemoveProductFromCart(int? CartId)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("cart_id", CartId)
                                               };

            var result = objGenericRepository.ExecuteSQL<int>("DeleteProductFromCart", sqlParameter).FirstOrDefault();
            return result;

        }

        public int? PlaceOrder(PlaceOrderEntity placeOrderObj)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                     new SqlParameter("store_id", placeOrderObj.store_id)
                                                   ,  new SqlParameter("customer_id", placeOrderObj.customer_id)
                                                   ,  new SqlParameter("shipping_addressId", placeOrderObj.shipping_addressId)
                                                   ,  new SqlParameter("ip", placeOrderObj.IpAddress!=null? placeOrderObj.IpAddress:(object)DBNull.Value)
                                                   ,  new SqlParameter("Comment", placeOrderObj.Comment!=null?placeOrderObj.Comment:(object)DBNull.Value)
                                                   ,  new SqlParameter("CartGroupId", placeOrderObj.CartGroupId)
                                               };




            var result = objGenericRepository.ExecuteSQL<int>("PlaceOrderAfterConfirmation", sqlParameter).FirstOrDefault();
            SendInvoiceMailToCustomer(result);
            return result;

        }
        public List<AddressEntity> GetCustmoreAddressList(int? store_id, int customer_id)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("store_id", store_id)
                                                 , new SqlParameter("customer_id", customer_id)
                                               };

            var result = objGenericRepository.ExecuteSQL<AddressEntity>("GetCustmoreAddressList", sqlParameter).ToList();
            return result;
        }




        public bool SendInvoiceMailToCustomer(int order_id)
        {

            if (order_id <= 0)
            {
                return false;
            }

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
                                              Point
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

            var filepath = HttpContext.Current.Server.MapPath("~/EmailTemplate/EmailTemplate.html");

            string strSubject = orderItemDetailsRecord.store_name + " -  Order " + Convert.ToString(orderItemDetailsRecord.order_id);
            var strbody = CommonService.ReadTextFile(filepath);
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
                strbody = strbody.Replace("##Comment##", orderItemDetailsRecord.Comment);
            }

            var commonServices = new CommonService();

            CommonService.SendMailMessage(orderItemDetailsRecord.email, null, null, strSubject, strbody, commonServices.GetEmailSettings(), null, orderItemDetailsRecord.store_name);
            return true;
        }



        // READ TEXT FILE
        //public static string ReadTextFile(string strFilePath)
        //{
        //    var entireFile = string.Empty;
        //    StreamReader objectRead = null;

        //    try
        //    {
        //        ////open text file
        //        objectRead = File.OpenText(strFilePath);
        //        entireFile = objectRead.ReadToEnd();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        Security.DisposeOf(objectRead);
        //    }

        //    return entireFile;
        //}


        //Send mail
        //public static bool SendMailMessage(string recipient, string bcc, string cc, string subject, string body, EmailNotification emailSetting, string attachment)
        //{
        //    if (string.IsNullOrEmpty(recipient))
        //    {
        //        return true;
        //    }

        //    // Instantiate a new instance of MailMessage 
        //    MailMessage mailMessage = new MailMessage();

        //    // Set the sender address of the mail message 
        //    mailMessage.From = new MailAddress(emailSetting.FromEmail, emailSetting.FromName);

        //    // Set the recipient address of the mail message 
        //    // mailMessage.To.Add(new MailAddress(recipient));
        //    if (!string.IsNullOrEmpty(recipient))
        //    {
        //        string[] strRecipient = recipient.Replace(";", ",").TrimEnd(',').Split(new char[] { ',' });

        //        // Set the Bcc address of the mail message 
        //        for (int intCount = 0; intCount < strRecipient.Length; intCount++)
        //        {
        //            mailMessage.To.Add(new MailAddress(strRecipient[intCount]));
        //        }
        //    }

        //    // Check if the bcc value is nothing or an empty string 
        //    if (!string.IsNullOrEmpty(bcc))
        //    {
        //        string[] strBCC = bcc.Split(new char[] { ',' });

        //        // Set the Bcc address of the mail message 
        //        for (int intCount = 0; intCount < strBCC.Length; intCount++)
        //        {
        //            mailMessage.Bcc.Add(new MailAddress(strBCC[intCount]));
        //        }
        //    }

        //    // Check if the cc value is nothing or an empty value 
        //    if (!string.IsNullOrEmpty(cc))
        //    {
        //        // Set the CC address of the mail message 
        //        string[] strCC = cc.Split(new char[] { ',' });
        //        for (int intCount = 0; intCount < strCC.Length; intCount++)
        //        {
        //            mailMessage.CC.Add(new MailAddress(strCC[intCount]));
        //        }
        //    }

        //    // Set the subject of the mail message 
        //    mailMessage.Subject = subject;

        //    // Set the body of the mail message 
        //    mailMessage.Body = body;

        //    // Set the format of the mail message body as HTML 
        //    mailMessage.IsBodyHtml = true;

        //    // Set the priority of the mail message to normal 
        //    mailMessage.Priority = MailPriority.Normal;

        //    // Instantiate a new instance of SmtpClient 
        //    var smtpClient = new SmtpClient();

        //    if (attachment != null && attachment != "")
        //        mailMessage.Attachments.Add(new Attachment(attachment));
        //    try
        //    {
        //        smtpClient.EnableSsl = emailSetting.EmailEnableSsl;
        //        smtpClient.Host = emailSetting.EmailHostName;
        //        smtpClient.Port = emailSetting.EmailPort;
        //        smtpClient.Credentials = new System.Net.NetworkCredential(emailSetting.EmailUsername, emailSetting.EmailPassword);

        //        // Send the mail message 
        //        smtpClient.Send(mailMessage);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        Security.DisposeOf(mailMessage);
        //        Security.DisposeOf(smtpClient);
        //    }
        //}


    }
}
