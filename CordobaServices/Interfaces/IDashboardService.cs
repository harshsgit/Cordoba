using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface IDashboardService
    {
        List<OrderEntity> GetLatestOrderDetailsDashboard(int storeId);

        DashboardSummaryEntity GetDashboardTopHeaderFields(int storeId);

        DashboardSummaryEntity GetDashboardSummaryCharts(int storeId, int ChartFiltertype, int ChartOrFunctionTypeEnum);
    }
}
