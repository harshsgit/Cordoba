using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
   public interface ICountryServices
    {
       List<CountryEntity> GetCountryList(int StoreId, int LoggedInUserId, int countryId);

       int InsertOrUpdateCountry(int StoreId, int LoggedInUserId, CountryEntity objCountry);

       int DeleteCountry(int StoreId, int LoggedInUserId, int countryId);
    }
}
