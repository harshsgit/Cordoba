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
    public class ActivityService : IActivityService
    {
        private GenericRepository<ActivityEntity> ActivityEntityGenericRepository = new GenericRepository<ActivityEntity>();
        
        public List<ActivityEntity> GetActivityList(int store_id)
        {
            try
            {
                var paramStoreId = new SqlParameter
                {
                    ParameterName = "store_id",
                    DbType = DbType.Int32,
                    Value = store_id
                };
                var activityEntity = ActivityEntityGenericRepository.ExecuteSQL<ActivityEntity>("GetActivityList",paramStoreId).ToList<ActivityEntity>().ToList();
                return activityEntity;
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
