using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface IContactUsService
    {
        bool sendContactUsDetails(string firstname, string lastname, string email, string phone, string description, StoreEntity storeEntity);
    }
}
