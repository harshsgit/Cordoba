using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class CategoryDescriptionList
    {
        public int? category_id { get; set; }
        public int? language_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string CategoryDescription { get; set; }
        public string meta_keywords { get; set; }
        public string meta_description { get; set; }
    }
}
