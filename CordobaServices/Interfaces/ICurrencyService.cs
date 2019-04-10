using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ICurrencyService
    {
        IList<CurrencyEntity> GetCurrencyList(int StoreId, int LoggedInUserId);
        CurrencyEntity GetCurrencyDetail(int StoreId, int LoggedInUserId, int currencyID = 0);

        int CreateOrUpdateCurrency(int StoreId, int LoggedInUserId, CurrencyEntity currency);

        int DeleteCurrency(int StoreId, int LoggedInUserId, int CurrencyId);
    }
}
