using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaCommon.Enum
{
    public static class CommonEnums
    {
        public enum ChartOrFunctionTypeEnum
        {
            All = 0
           ,
            OrderSummary = 1
                ,
            SalesAnalytics = 2
                ,
            Top5SellingStores = 3
                , Top5Customers = 4
        }

        public enum FolderName
        {
            Category = 1,
            StoreImage = 2,
            BannerImage = 3,
            CustomerImage = 4,
            store_logos = 5,
            productImage = 6
        }
    }
}
