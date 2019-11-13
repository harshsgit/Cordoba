
using CordobaCommon;
using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Helpers;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;


namespace CordobaServices.Services
{

    public class CustomerService : ICustomerService
    {

        private GenericRepository<CustomerEntity> CustomerEntityGenericRepository = new GenericRepository<CustomerEntity>();

        public List<CustomerEntity> GetCustomerList(string sortColumn, TableParameter<CustomerEntity> filter, string customerName, string email, int? customer_group_id, int? status, int? approved, string ip, DateTime? date_added, int? storeId, long? UserId)
        {
            try
            {
                var paramOrderBy = new SqlParameter { ParameterName = "OrderBy", DbType = DbType.String, Value = sortColumn };
                var paramPageSize = new SqlParameter { ParameterName = "PageSize", DbType = DbType.Int32, Value = filter != null ? filter.iDisplayLength : 10 };
                var paramPageIndex = new SqlParameter { ParameterName = "PageIndex", DbType = DbType.Int32, Value = filter != null ? filter.PageIndex : 1 };

                var paramCustomerName = new SqlParameter { ParameterName = "customerName", DbType = DbType.String, Value = customerName ?? DBNull.Value.ToString() };
                var paramEmail = new SqlParameter { ParameterName = "email", DbType = DbType.String, Value = email ?? (object)DBNull.Value };
                var paramCustomer_group_id = new SqlParameter { ParameterName = "customer_group_id", DbType = DbType.Int32, Value = customer_group_id ?? (object)DBNull.Value };
                var paramApproved = new SqlParameter { ParameterName = "approved", DbType = DbType.Int32, Value = approved ?? (object)DBNull.Value };
                var paramIp = new SqlParameter { ParameterName = "ip", DbType = DbType.String, Value = ip ?? (object)DBNull.Value };
                var paramDate_added = new SqlParameter { ParameterName = "date_added", DbType = DbType.DateTime, Value = date_added ?? (object)DBNull.Value };
                var paramstatus = new SqlParameter { ParameterName = "status", DbType = DbType.Int32, Value = status ?? (object)DBNull.Value };
                var paramStoreId = new SqlParameter { ParameterName = "storeId", DbType = DbType.Int32, Value = storeId ?? (object)DBNull.Value };
                var paramUserId = new SqlParameter { ParameterName = "UserId", DbType = DbType.Int32, Value = UserId ?? (object)DBNull.Value };
                var CustomerList = CustomerEntityGenericRepository.ExecuteSQL<CustomerEntity>("EXEC GetCustomerList", paramOrderBy, paramPageSize, paramPageIndex, paramCustomerName, paramEmail, paramCustomer_group_id, paramApproved, paramIp, paramDate_added, paramstatus, paramStoreId, paramUserId).ToList<CustomerEntity>().ToList();
                return CustomerList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DataSet CustomerExportToExcel(string sortColumn, object filter, string customerName, string email, int? customer_group_id, int? status, int? approved, string ip, DateTime? date_added, int? storeId, long? UserId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                cmd = new SqlCommand("GetCustomerList", con);
                cmd.Parameters.Add(new SqlParameter("@OrderBy", sortColumn));
                cmd.Parameters.Add(new SqlParameter("@PageSize", 10000000));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", 1));
                cmd.Parameters.Add(new SqlParameter("@customerName", customerName != null ? customerName : (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@email", email));
                cmd.Parameters.Add(new SqlParameter("@customer_group_id", customer_group_id));
                cmd.Parameters.Add(new SqlParameter("@approved", approved ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ip", ip));
                cmd.Parameters.Add(new SqlParameter("@date_added", date_added ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@status", status ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@storeId", storeId ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId ?? (object)DBNull.Value));
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

        public CustomerEntity GetCustomerById(int StoreId, int LoggedInUserId, int customer_id)
        {
            try
            {
                CustomerEntity customerEntity = new CustomerEntity();
                List<AddressEntity> addressEntity = new List<AddressEntity>();
                List<PointsAuditEntity> PointsAuditList = new List<PointsAuditEntity>();
                List<TransactionListEntity> TransactionList = new List<TransactionListEntity>();

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

                if (customer_id > 0)
                {
                    var paramCustomer_id = new SqlParameter { ParameterName = "customer_id", DbType = DbType.Int32, Value = customer_id };
                    var result = CustomerEntityGenericRepository.ExecuteSQL<CustomerEntity>("EXEC GetCustomerById", paramCustomer_id).FirstOrDefault();
                    if (result != null)
                    {
                        customerEntity = result;
                        customerEntity.password = Security.Decrypt(result.password);
                    }


                    #region customer Address
                    var AddressResult = CustomerEntityGenericRepository.ExecuteSQL<AddressEntity>("EXEC GetCustomerAddressList", new SqlParameter { ParameterName = "customer_id", DbType = DbType.Int32, Value = customer_id }).ToList<AddressEntity>().ToList();
                    if (AddressResult != null)
                    {
                        addressEntity = AddressResult;
                    }

                    #endregion

                    #region  PointsAuditEntity

                    var PointsAuditResult = CustomerEntityGenericRepository.ExecuteSQL<PointsAuditEntity>("EXEC GetPointsAuditDetail", new SqlParameter { ParameterName = "Customer_Id", DbType = DbType.Int32, Value = customer_id }).ToList<PointsAuditEntity>().ToList();
                    if (AddressResult != null)
                    {
                        PointsAuditList = PointsAuditResult;
                    }
                    #endregion

                    #region Transaction List
                    var TransactionListResult = CustomerEntityGenericRepository.ExecuteSQL<TransactionListEntity>("EXEC GetTransactionList", new SqlParameter { ParameterName = "Customer_Id", DbType = DbType.Int32, Value = customer_id }).ToList<TransactionListEntity>().ToList();
                    if (TransactionListResult != null)
                    {
                        TransactionList = TransactionListResult;
                    }
                    #endregion
                    customerEntity.AddressList = addressEntity;
                    customerEntity.PointsAuditList = PointsAuditList;
                    customerEntity.TransactionList = TransactionList;
                }
                else
                {
                    customerEntity = new CustomerEntity();
                    customerEntity.AddressList = addressEntity = new List<AddressEntity>();
                    customerEntity.PointsAuditList = new List<PointsAuditEntity>();
                }




                return customerEntity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public int InsertUpdateCustomer(int LoggedInUserId, CustomerEntity customerEntity)
        {
            string AddressXml = Helpers.ConvertToXml<AddressEntity>.GetXMLString(customerEntity.AddressList);

            string PointsAuditXml = Helpers.ConvertToXml<PointsAuditEntity>.GetXMLString(customerEntity.PointsAuditList);

            SqlParameter[] sqlParameter = new SqlParameter[] {

                                                   new SqlParameter("customer_id", customerEntity.customer_id)
                                                 , new SqlParameter("store_id", customerEntity.store_id ?? (object) DBNull.Value)
                                                 , new SqlParameter("firstname", customerEntity.firstname ??  DBNull.Value.ToString())
                                                 , new SqlParameter("lastname", customerEntity.lastname ??  DBNull.Value.ToString())
                                                 , new SqlParameter("email", customerEntity.email ??  DBNull.Value.ToString())
                                                 , new SqlParameter("telephone", customerEntity.telephone ??   DBNull.Value.ToString())
                                                 , new SqlParameter("password", Security.Encrypt(customerEntity.password) ??  DBNull.Value.ToString())
                                                 , new SqlParameter("status", customerEntity.status)
                                                 , new SqlParameter("approved", customerEntity.approved)
                                                 , new SqlParameter("activated", customerEntity.activated)
                                                 , new SqlParameter("is_admin", customerEntity.is_admin)
                                                 , new SqlParameter("customer_department_id", customerEntity.customer_department_id.HasValue?customerEntity.customer_department_id.Value :(object)DBNull.Value)
                                                 , new SqlParameter("AddressXml", AddressXml ??  (object)DBNull.Value)
                                                 , new SqlParameter("PointsAuditXml", PointsAuditXml ??  (object)DBNull.Value)
                                                 , new SqlParameter("is_department", (object)customerEntity.is_department??DBNull.Value)
                                                };
            int result = CustomerEntityGenericRepository.ExecuteSQL<int>("InsertUpdateCustomer", sqlParameter).FirstOrDefault();
            return result;
        }




        public int DeleteCustomer(int StoreId, int LoggedInUserId, int customer_id)
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
                var paramId = new SqlParameter { ParameterName = "customer_id", DbType = DbType.Int32, Value = customer_id };
                int result = CustomerEntityGenericRepository.ExecuteSQL<int>("DeleteCustomer", paramId).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<PointsAuditEntity> PointsImporter(int store_id, int LoggedInUserId, bool IsSendEmail, DataTable PointsTable)
        {
            string PointsXml = GeneralMethods.ConvertDatatableToXML(PointsTable);
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("store_id", store_id);
                param[1] = new SqlParameter("IsSendEmail", IsSendEmail);
                param[2] = new SqlParameter("PointsXml", PointsXml);

                List<PointsAuditEntity> result = CustomerEntityGenericRepository.ExecuteSQL<PointsAuditEntity>("EXEC ImportPointsXml", param).ToList();
                //if (IsSendEmail && string.IsNullOrWhiteSpace(result.FirstOrDefault().invalidEmail))
                if (IsSendEmail)
                {
                    SendPointImportMailToCustomer(PointsXml, store_id);
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Review Point On Popup
        public List<PointReviewEntity> PointsImportOnPopup(int store_id, int LoggedInUserId, bool IsSendEmail, DataTable PointsTable)
        {
            string PointsXml = GeneralMethods.ConvertDatatableToXML(PointsTable);

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("store_id", store_id);
            param[1] = new SqlParameter("IsSendEmail", IsSendEmail);
            param[2] = new SqlParameter("PointsXml", PointsXml);

            return CustomerEntityGenericRepository.ExecuteSQL<PointReviewEntity>("EXEC PointsImportOnPopup", param).ToList();

        }
        //GetauditPointTMP
        public List<auditPointTMP> GetauditPointTMP()
        {
            return CustomerEntityGenericRepository.ExecuteSQL<auditPointTMP>("EXEC GetauditPointTMP").ToList();
        }
        //Import Point From TMP Table
        public bool ImportPointFromTMP(string uniqueNo, int store, bool sendMail = false)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("UniqueNo", uniqueNo);
                param[1]=new SqlParameter("store", store);
                var listPointsOfCustomer = CustomerEntityGenericRepository.ExecuteSQL<CustomerPointDetail>("EXEC ImportPointFromTMP", param).ToList();
                if ((listPointsOfCustomer != null && listPointsOfCustomer.Count > 0) && sendMail == true)
                {

                    var filepath = HttpContext.Current.Server.MapPath("~/EmailTemplate/ImportedPoint.html");
                    if (listPointsOfCustomer.Count > 0)
                    {
                        for (int i = 0; i < listPointsOfCustomer.Count; i++)
                        {
                            string strSubject = listPointsOfCustomer[i].StoreName + " -  Reward Points";
                            var strbody = CommonService.ReadTextFile(filepath);
                            if (strbody.Length > 0)
                            {
                                strbody = strbody.Replace("##CustomerName##", listPointsOfCustomer[i].CustomerName);//
                                strbody = strbody.Replace("##StoreName##", String.Format("<a href={0}>{1}</a>", listPointsOfCustomer[i].URL, listPointsOfCustomer[i].StoreName));
                                strbody = strbody.Replace("##ImportedPoints##", Convert.ToString(listPointsOfCustomer[i].ImportedPoints));
                                strbody = strbody.Replace("##TotalPoints##", Convert.ToString(listPointsOfCustomer[i].TotalPoints));
                            }

                            var commonServices = new CommonService();

                            CommonService.SendMailMessage(listPointsOfCustomer[i].EmailAddress, null, null, strSubject, strbody, commonServices.GetEmailSettings(), null, listPointsOfCustomer[i].StoreName);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public bool SendPointImportMailToCustomer(string PointsXml, int storeid)
        {
            if (storeid <= 0)
            {
                return false;
            }

            var storeIdParam = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = storeid
            };

            var pointsXmlParam = new SqlParameter
            {
                ParameterName = "PointsXml",
                DbType = DbType.String,
                Value = PointsXml
            };

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("StoreId", storeid);
            param[1] = new SqlParameter("PointsXml", PointsXml);

            var result = CustomerEntityGenericRepository.ExecuteSQL<ImportedPointEmailEntity>("GetExistingAndImportedPointsForEmail", pointsXmlParam, storeIdParam);

            var listPointsOfCustomer = result.ToList();

            var filepath = HttpContext.Current.Server.MapPath("~/EmailTemplate/ImportedPoint.html");
            if (listPointsOfCustomer.Count > 0)
            {
                for (int i = 0; i < listPointsOfCustomer.Count; i++)
                {
                    string strSubject = listPointsOfCustomer[i].StoreName + " -  Reward Points";
                    var strbody = CommonService.ReadTextFile(filepath);
                    if (strbody.Length > 0)
                    {
                        strbody = strbody.Replace("##CustomerName##", listPointsOfCustomer[i].CustomerName);//
                        strbody = strbody.Replace("##StoreName##", String.Format("<a href={0}>{1}</a>", listPointsOfCustomer[i].URL, listPointsOfCustomer[i].StoreName));
                        strbody = strbody.Replace("##ImportedPoints##", Convert.ToString(listPointsOfCustomer[i].ImportedPoints));
                        strbody = strbody.Replace("##TotalPoints##", Convert.ToString(listPointsOfCustomer[i].TotalPoints));
                    }

                    var commonServices = new CommonService();

                    CommonService.SendMailMessage(listPointsOfCustomer[i].Email, null, null, strSubject, strbody, commonServices.GetEmailSettings(), null, listPointsOfCustomer[i].StoreName);
                }
            }

            return true;
        }

        public string CustomerImport(int store_id, int LoggedInUserId, int customer_group_id, DataTable CustomerTable, string UserPassword)
        {
            string CustomerXml = GeneralMethods.ConvertDatatableToXML(CustomerTable);
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("store_id", store_id);
                param[1] = new SqlParameter("LoggedInUserId", LoggedInUserId);
                param[2] = new SqlParameter("customer_group_id", customer_group_id);
                param[3] = new SqlParameter("CustomerXml", CustomerXml);

                //var result = CustomerEntityGenericRepository.ExecuteSQL<string>("EXEC ImportCustomerXml", param).FirstOrDefault();
                var listPasswordOfCustomer = CustomerEntityGenericRepository.ExecuteSQL<ImportCustomerEmailEntity>("EXEC ImportCustomerXml", param).ToList();
                //if (listPasswordOfCustomer != null && listPasswordOfCustomer.Count > 0)
                if (string.IsNullOrWhiteSpace(listPasswordOfCustomer.FirstOrDefault().ErrorLog))
                {
                    SendCustomerImportMailToCustomer(listPasswordOfCustomer, store_id, UserPassword);
                }
                var result = string.IsNullOrWhiteSpace(listPasswordOfCustomer.FirstOrDefault().ErrorLog) ? string.Empty : listPasswordOfCustomer.FirstOrDefault().ErrorLog;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SendCustomerImportMailToCustomer(List<ImportCustomerEmailEntity> listPasswordOfCustomer, int storeid, string UserPassword)
        {
            if (storeid <= 0)
            {
                return false;
            }

            var storeIdParam = new SqlParameter
            {
                ParameterName = "store_id",
                DbType = DbType.Int32,
                Value = storeid
            };

            //var customerXmlParam = new SqlParameter
            //{
            //    ParameterName = "CustomerXml",
            //    DbType = DbType.String,
            //    Value = CustomerXml
            //};

            // var result = CustomerEntityGenericRepository.ExecuteSQL<ImportCustomerEmailEntity>("GetCustomerEmailAndPasswordForEmail", storeIdParam, customerXmlParam);

            // var listPasswordOfCustomer = result.ToList();

            var filepath = HttpContext.Current.Server.MapPath("~/EmailTemplate/ImportCustomer.html");
            if (listPasswordOfCustomer.Count > 0)
            {
                for (int i = 0; i < listPasswordOfCustomer.Count; i++)
                {
                    string strSubject = string.Format("welcome to {0}", listPasswordOfCustomer[i].StoreName);
                    var strbody = CommonService.ReadTextFile(filepath);
                    if (strbody.Length > 0)
                    {
                        listPasswordOfCustomer[i].StoreName = Regex.Replace(listPasswordOfCustomer[i].StoreName, "rewards", "", RegexOptions.IgnoreCase);
                        strbody = strbody.Replace("##CustomerName##", listPasswordOfCustomer[i].CustomerName);
                        strbody = strbody.Replace("##Password##", UserPassword);
                        strbody = strbody.Replace("##StoreName##", !string.IsNullOrEmpty(listPasswordOfCustomer[i].StoreName) ? listPasswordOfCustomer[i].StoreName.ToUpper().TrimEnd() : "");
                        strbody = strbody.Replace("##StoreNameLink##", string.Format("<a href={0}>{1}</a>", listPasswordOfCustomer[i].URL, listPasswordOfCustomer[i].StoreName));
                    }

                    var commonServices = new CommonService();

                    CommonService.SendMailMessage(listPasswordOfCustomer[i].Email, null, null, strSubject, strbody, commonServices.GetEmailSettings(), null, listPasswordOfCustomer[i].StoreName);
                }
            }

            return true;
        }

        public bool UploadUserImage(int customerImage_id, int customer_id, string ImageName)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("customerImage_id", customerImage_id),
                    new SqlParameter("customer_id", customer_id),
                    new SqlParameter("image", ImageName)
                };
                int result = CustomerEntityGenericRepository.ExecuteSQL<int>("AddOrUpdateCustomerImage", sqlParameter).FirstOrDefault();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomerImageEntity> getUserImage(int customer_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("customer_id", customer_id),
                 };

                var CustomerList = CustomerEntityGenericRepository.ExecuteSQL<CustomerImageEntity>("GetUserImage", sqlParameter).ToList<CustomerImageEntity>().ToList();
                return CustomerList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int deleteCustomerImage(int customer_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("customer_id", customer_id)
                };
                var result = CustomerEntityGenericRepository.ExecuteSQL<int>("DeleteCustomerImage", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int InsertPointAudit(int customer_id, string description, int points)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("customer_id", customer_id),
                    new SqlParameter("description", description),
                    new SqlParameter("points", points)
                };
                var result = CustomerEntityGenericRepository.ExecuteSQL<int>("InsertPointAudit", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public CustomerEntity SendCustomerPassword(int customer_id, string NewPassword)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("customer_id", customer_id),
                    new SqlParameter("password", Security.Encrypt(NewPassword))
                };
                var result = CustomerEntityGenericRepository.ExecuteSQL<CustomerEntity>("GetCustomerPassword", sqlParameter).FirstOrDefault();
                CommonService customerService = new CommonService();
                var result1 = customerService.SendCustomerPasswordEmail(result.email, NewPassword, result.firstname, result.store_name, result.store_url);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
