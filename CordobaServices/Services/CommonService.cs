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
using CordobaServices.Interfaces;


namespace CordobaServices.Services
{
    public class CommonService : ICommonServices
    {
        //Email Sending for order History
        private GenericRepository<PlaceOrderEntity> placeOrderRepository = new GenericRepository<PlaceOrderEntity>();

        public EmailNotification GetEmailSettings()
        {
            var objemailsetting = new EmailNotification();

            objemailsetting.EmailUsername = ConfigurationManager.AppSettings["emailusername"];
            objemailsetting.EmailPassword = ConfigurationManager.AppSettings["emailpassword"];
            objemailsetting.EmailHostName = ConfigurationManager.AppSettings["emailhostname"];
            objemailsetting.EmailEnableSsl = Convert.ToBoolean(Convert.ToInt32(ConfigurationManager.AppSettings["emailenablessl"]));
            objemailsetting.EmailPort = Convert.ToInt32(ConfigurationManager.AppSettings["emailport"]);
            objemailsetting.FromName = ConfigurationManager.AppSettings["emailusername"];
            objemailsetting.FromEmail = ConfigurationManager.AppSettings["emailusername"];

            return objemailsetting;
        }



        //Send mail
        public static bool SendMailMessage(string recipient, string bcc, string cc, string subject, string body, EmailNotification emailSetting, string attachment, string storeName)
        {
            //recipient = ConfigurationManager.AppSettings["emailTo"];
            if (string.IsNullOrEmpty(recipient))
            {
                return true;
            }

            // Instantiate a new instance of MailMessage 
            MailMessage mailMessage = new MailMessage();

            if (!string.IsNullOrWhiteSpace(storeName))
            {
                mailMessage.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["emailTo"], storeName);

                mailMessage.Sender = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["emailTo"], storeName);

                mailMessage.ReplyToList.Add(new MailAddress(System.Configuration.ConfigurationManager.AppSettings["emailTo"], storeName));
            }
            else
            {
                mailMessage.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["emailTo"]);

                mailMessage.Sender = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["emailTo"]);

                mailMessage.ReplyToList.Add(new MailAddress(System.Configuration.ConfigurationManager.AppSettings["emailTo"]));
            }

            // Set the sender address of the mail message 

            // Set the recipient address of the mail message 
            // mailMessage.To.Add(new MailAddress(recipient));
            if (!string.IsNullOrEmpty(recipient))
            {
                string[] strRecipient = recipient.Replace(";", ",").TrimEnd(',').Split(new char[] { ',' });

                // Set the Bcc address of the mail message 
                for (int intCount = 0; intCount < strRecipient.Length; intCount++)
                {
                    mailMessage.To.Add(new MailAddress(strRecipient[intCount]));
                }
            }

            // Check if the cc value is nothing or an empty value 
            //if (!string.IsNullOrEmpty(cc))
            //{
            //    // Set the CC address of the mail message 
            //    string[] strCC = cc.Split(new char[] { ',' });
            //    for (int intCount = 0; intCount < strCC.Length; intCount++)
            //    {
            //        mailMessage.CC.Add(new MailAddress(strCC[intCount]));
            //    }
            //}

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["emailCC"]))
            {
                string[] strBCC = ConfigurationManager.AppSettings["emailCC"].Split(new char[] { ',' });

                //Set the Bcc address of the mail message
                for (int intCount = 0; intCount < strBCC.Length; intCount++)
                {
                    mailMessage.CC.Add(new MailAddress(strBCC[intCount]));
                }
            }


            if ((!subject.ToLower().Contains("verify".ToString())) && (!subject.ToLower().Contains("reward points".ToString())) && (!subject.ToLower().Contains("welcome to".ToString())))
            {
                
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["emailBCC"]))
                {
                    string[] strBCC = ConfigurationManager.AppSettings["emailBCC"].Split(new char[] { ',' });

                    //Set the Bcc address of the mail message
                    for (int intCount = 0; intCount < strBCC.Length; intCount++)
                    {
                        mailMessage.Bcc.Add(new MailAddress(strBCC[intCount]));
                    }
                }
            }

            // Set the subject of the mail message 
            mailMessage.Subject = subject;

            // Set the body of the mail message 
            mailMessage.Body = body;

            // Set the format of the mail message body as HTML 
            mailMessage.IsBodyHtml = true;

            // Set the priority of the mail message to normal 
            mailMessage.Priority = MailPriority.Normal;

            // Instantiate a new instance of SmtpClient 
            var smtpClient = new SmtpClient();

            if (attachment != null && attachment != "")
                mailMessage.Attachments.Add(new Attachment(attachment));
            try
            {
                smtpClient.EnableSsl = emailSetting.EmailEnableSsl;
                smtpClient.Host = emailSetting.EmailHostName;
                smtpClient.Port = emailSetting.EmailPort;
                smtpClient.Credentials = new System.Net.NetworkCredential(emailSetting.EmailUsername, emailSetting.EmailPassword);


                // Send the mail message 
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                Security.DisposeOf(mailMessage);
                Security.DisposeOf(smtpClient);
            }
        }




        /// <summary>
        /// By Pavan Antala
        /// 23 JUNE 2017
        /// Sends the OTP Email to user for verification
        /// </summary>
        /// <param name="email"></param>
        /// <param name="otp"></param>
        /// <param name="name"></param>
        /// <param name="StoreName"></param>
        ///  <param name="logopath"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public bool SendOTPEmail(string email, string otp, string name, string store_name, string logopath)
        {
            string strSubject = store_name + " -  Verify Email";

            var filepath = HttpContext.Current.Server.MapPath("~/EmailTemplate/VerifyOTP.html");
            var strbody = ReadTextFile(filepath);

            if (strbody.Length <= 0)
                return false;

            strbody = strbody.Replace("##name##", name);
            strbody = strbody.Replace("##OTP##", otp);
            strbody = strbody.Replace("##StoreName##", store_name);
            strbody = strbody.Replace("##LogoPath##", logopath);

            return SendMailMessage(email, null, null, strSubject, strbody, GetEmailSettings(), null, store_name);
        }

        public bool SendCustomerPasswordEmail(string email, string password, string name, string store_name, string store_url)
        {
            string strSubject = store_name + " -  Password";

            var filepath = HttpContext.Current.Server.MapPath("~/EmailTemplate/SendPassword.html");
            var strbody = ReadTextFile(filepath);

            if (strbody.Length <= 0)
                return false;

            strbody = strbody.Replace("##name##", name);
            strbody = strbody.Replace("##Password##",password);
            strbody = strbody.Replace("##StoreName##", store_name);
            strbody = strbody.Replace("##store_url##", store_url);
            //strbody = strbody.Replace("##LogoPath##", logopath);

            return SendMailMessage(email, null, null, strSubject, strbody, GetEmailSettings(), null, store_name);
        }

        public bool SendResetPassOTPEmail(string email, string otp, string name, string store_name, string logopath)
        {
            string strSubject = store_name + " - Reset Password";

            var filepath = HttpContext.Current.Server.MapPath("~/EmailTemplate/VerifyOTP.html");
            var strbody = ReadTextFile(filepath);

            if (strbody.Length <= 0)
                return false;

            strbody = strbody.Replace("##name##", name);
            strbody = strbody.Replace("##OTP##", otp);
            strbody = strbody.Replace("##StoreName##", store_name);
            strbody = strbody.Replace("##LogoPath##", logopath);

            return SendMailMessage(email, null, null, strSubject, strbody, GetEmailSettings(), null, store_name);
        }

        public bool sendContactUsDetails(string name, string email, string phone, string description, StoreEntity storeEntity)
        {
            string strSubject = storeEntity.name + " - Inquiry";

            var filepath = HttpContext.Current.Server.MapPath("~/EmailTemplate/ContactUs.html");
            var strbody = ReadTextFile(filepath);

            if (strbody.Length <= 0)
                return false;

            strbody = strbody.Replace("##Name##", name);
            strbody = strbody.Replace("##Email##", email);
            strbody = strbody.Replace("##Phone##", phone);
            strbody = strbody.Replace("##Query##", description);
            strbody = strbody.Replace("##LogoPath##", storeEntity.logo);
            strbody = strbody.Replace("##StoreName##", storeEntity.name);

            return SendMailMessage(ConfigurationManager.AppSettings["emailTo"], null, null, strSubject, strbody, GetEmailSettings(), null, storeEntity.name);

        }


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
