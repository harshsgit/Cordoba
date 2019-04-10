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
    public class TestServices : ITestServices
    {
       private GenericRepository<TestEntity> objTestEntityGenericRepository = new GenericRepository<TestEntity>();
       public  int?  InsertOrUpdateCompanyLayout(TestEntity model)
       {


         

           //var CompanyLayoutId = new SqlParameter { ParameterName = "CompanyLayoutId", DbType = DbType.Int32, Value = model.CompanyLayoutId > 0 ? model.CompanyLayoutId : (object)DBNull.Value };
           //var HostName = new SqlParameter { ParameterName = "HostName", DbType = DbType.String, Value = model.HostName };
           //var LayoutName = new SqlParameter { ParameterName = "LayoutName", DbType = DbType.String, Value = model.LayoutName };
           ////var Description = new SqlParameter { ParameterName = "Description", DbType = DbType.String, Value = model.Description };
           //var CreatedBy = new SqlParameter { ParameterName = "CreatedBy", DbType = DbType.Int32, Value = model.CreatedBy };          
           //var result = objTestEntityGenericRepository.ExecuteSQL<int>("Insert_Update_CompanyLayout", CompanyLayoutId, HostName, LayoutName, CreatedBy).ToList<int>().FirstOrDefault();
           try
           {
               SqlParameter[] param = new SqlParameter[4];
               param[0] = new SqlParameter("CompanyLayoutId", model.CompanyLayoutId > 0 ? model.CompanyLayoutId : (object)DBNull.Value);
               param[1] = new SqlParameter("HostName", model.HostName);
               param[2] = new SqlParameter("LayoutName", model.LayoutName);
               param[3] = new SqlParameter("CreatedBy", model.CreatedBy);

               var result = objTestEntityGenericRepository.ExecuteSQL<int>("Insert_Update_CompanyLayout @CompanyLayoutId,@HostName,@LayoutName,@CreatedBy", param).ToList<int>().FirstOrDefault();
               return result;
           }
           catch (Exception)
           {

               throw;
           }
     
       }
    }
}
