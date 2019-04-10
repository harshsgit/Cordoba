using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ILanguageService
    {
        List<LanguageEntity> GetLanguageList(int? languageId, int StoreId, int LoggedInUserId);

        int InsertOrUpdateLanguage(int StoreId, int LoggedInUserId, LanguageEntity objEntity);

        int DeleteLanguage(int StoreId, int LoggedInUserId, int languageId);
    }
}
