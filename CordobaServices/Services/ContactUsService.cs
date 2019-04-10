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

namespace CordobaServices.Services
{
    public class ContactUsService : IContactUsService
    {
        private GenericRepository<BannerEntity> contactUsGenericRepository = new GenericRepository<BannerEntity>();

        public bool sendContactUsDetails(string firstname, string lastname, string email, string phone, string description, StoreEntity storeEntity)
        {
            try
            {
                CommonService customerService = new CommonService();
                return customerService.sendContactUsDetails(firstname + " " + lastname, email, phone, description, storeEntity);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
