
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
    public class CountryServices : ICountryServices
    {
        private GenericRepository<CountryEntity> objGenericRepository = new GenericRepository<CountryEntity>();

        public List<CountryEntity> GetCountryList(int StoreId, int LoggedInUserId, int countryId)
        {
            List<CountryEntity> Countries = new List<CountryEntity>();
            var paramCountryId = new SqlParameter 
            { 
                ParameterName = "countryId", 
                DbType = DbType.Int32, 
                Value = countryId 
            };
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
            Countries = objGenericRepository.ExecuteSQL<CountryEntity>("GetCountryList", paramCountryId,ParameterStoreId,ParameterLoggedInUserId).ToList();          
            return Countries;
        }

        public int InsertOrUpdateCountry(int StoreId, int LoggedInUserId, CountryEntity objCountry)
        {
            var paramCountryId = new SqlParameter 
            { 
                ParameterName = "countryId", 
                DbType = DbType.Int32, 
                Value = Convert.ToInt32(objCountry.country_id) 
            };
            var paramCountryCd = new SqlParameter 
            { 
                ParameterName = "countryCd", 
                DbType = DbType.String, 
                Value = objCountry.iso_code_2.Trim() ?? (object)DBNull.Value 
            };
            var paramCountryName = new SqlParameter 
            { 
                ParameterName = "countryName", 
                DbType = DbType.String, 
                Value = objCountry.name ?? (object)DBNull.Value 
            };
            var paramstatus = new SqlParameter 
            { 
                ParameterName = "status", 
                DbType = DbType.Int32, 
                Value = objCountry.Status 
            };
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
            int result = objGenericRepository.ExecuteSQL<int>("InsertOrUpdateCountry", paramCountryId, paramCountryCd, paramCountryName, paramstatus, ParameterStoreId, ParameterLoggedInUserId).FirstOrDefault();
            return result;
        }

        public int DeleteCountry(int StoreId, int LoggedInUserId, int countryId)
        {
            var paramcountryId = new SqlParameter 
            { 
                ParameterName = "countryId", 
                DbType = DbType.Int32, 
                Value = countryId 
            };
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
            int result = objGenericRepository.ExecuteSQL<int>("DeleteCountry", paramcountryId, ParameterStoreId, ParameterLoggedInUserId).FirstOrDefault();
            return result;
        }
    }
}
