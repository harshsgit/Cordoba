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
    public class PointsAuditService : IPointsAudit
    {
        private GenericRepository<PointsAuditEntity> objGenericRepository = new GenericRepository<PointsAuditEntity>();

        public List<PointsAuditEntity> GetPointsAuditList(int Customer_Id)
        {
            try
            {
                var paramCustomer = new SqlParameter { ParameterName = "Customer_Id", DbType = DbType.Int32, Value = Customer_Id };
                var query = objGenericRepository.ExecuteSQL<PointsAuditEntity>("GetPointsAuditDetail", paramCustomer).ToList<PointsAuditEntity>();
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
