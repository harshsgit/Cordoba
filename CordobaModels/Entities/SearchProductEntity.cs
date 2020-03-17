using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class SearchProductEntity
    {
        public int StoreID { get; set; }
        public int CategoryId { get; set; }
        public int PageIndex { get; set; }
        public int Customer_Id { get; set; } = 0;
        public string WhatAreYouLookingFor { get; set; } = "";
        public string SearchByFilterId { get; set; } = "";
        public int OrderById { get; set; } = 1;
    }
}
