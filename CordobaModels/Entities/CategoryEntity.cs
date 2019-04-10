using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class CategoryEntity
    {
        public int Category_Id { get; set; }
        public string name { get; set; }
        public string CategoryName { get; set; }
        public int? sort_order { get; set; }
        public int? CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        //public CategoryStoreEntity CategoryStoreList { get; set; }
        
        public Nullable<int> parentGroupId { get; set; }
        public string ParentCategoryName { get; set; }
        public int? status { get; set; }
        public int? storeId { get; set; }
        public int? language_id { get; set; }
        public string description { get; set; }

        public string StoreIdCSV { get; set; }
        public List<CategoryDescriptionList> CategoryDescriptionList { get; set; }
        public List<StoreEntity> StoreList { get; set; }
        // need to remove 
     
        public int parent_Id { get; set; }
        public string image { get; set; }

        public int? ReportCategoryId{ get; set; }
        
  
      
        
        
        

    }
}
