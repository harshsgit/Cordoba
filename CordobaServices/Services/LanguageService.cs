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
    public class LanguageService : ILanguageService
    {
        private GenericRepository<LanguageEntity> objGenericRepository = new GenericRepository<LanguageEntity>();

        public List<LanguageEntity> GetLanguageList(int? languageId, int StoreId, int LoggedInUserId)
        {
            List<LanguageEntity> languages = new List<LanguageEntity>();
            var paramLanguageId = new SqlParameter 
            { 
                ParameterName = "language_id", 
                DbType = DbType.Int32, 
                Value = languageId 
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
            languages = objGenericRepository.ExecuteSQL<LanguageEntity>("GetLanguageList",ParameterStoreId,ParameterLoggedInUserId,paramLanguageId).ToList();
            return languages;
        }

        public int InsertOrUpdateLanguage(int StoreId, int LoggedInUserId, LanguageEntity objEntity)
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
            var paramlanguageId = new SqlParameter 
            { 
                ParameterName = "languageId", 
                DbType = DbType.Int32, 
                Value = Convert.ToInt32(objEntity.language_id) 
            };
            var paramlanguageCd = new SqlParameter 
            { 
                ParameterName = "languageCd", 
                DbType = DbType.String, 
                Value = objEntity.code.Trim()
            };
            var paramsort_order = new SqlParameter 
            { 
                ParameterName = "sort_order", 
                DbType = DbType.Int32, 
                Value = objEntity.sort_order 
            };
            var paramlanguageName = new SqlParameter 
            { 
                ParameterName = "languageName", 
                DbType = DbType.String, 
                Value = objEntity.name 
            };
            var paramimage = new SqlParameter 
            { 
                ParameterName = "image", 
                DbType = DbType.String, 
                Value = objEntity.image 
            };
            var paramstatus = new SqlParameter 
            { 
                ParameterName = "status", 
                DbType = DbType.Int32, 
                Value = objEntity.status 
            };

            var result = objGenericRepository.ExecuteSQL<int>("InsertOrUpdateLanguage", paramlanguageId, paramlanguageCd, paramsort_order, paramlanguageName, paramimage, paramstatus ,ParameterStoreId, ParameterLoggedInUserId).FirstOrDefault();
            return result;
        }

        public int DeleteLanguage(int StoreId, int LoggedInUserId, int languageId)
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
            var paramLanguageId = new SqlParameter 
            { 
                ParameterName = "languageId", 
                DbType = DbType.Int32, 
                Value = Convert.ToInt32(languageId) 
            };
            int result = objGenericRepository.ExecuteSQL<int>("DeleteLanguage", ParameterStoreId, ParameterLoggedInUserId, paramLanguageId).FirstOrDefault();
            return result;
        }
    }
}
