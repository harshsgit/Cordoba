using CordobaAPI.Utility;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class CommonServices : ICommonServices
    {
        private GenericRepository<CompanyLayoutEntity> objTestEntityGenericRepository = new GenericRepository<CompanyLayoutEntity>();

        public string GetLayoutName(string HostName)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@HostName", HostName);

                var result = objTestEntityGenericRepository.ExecuteSQL<string>("GetLayoutName @HostName", param).ToList<string>().FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
