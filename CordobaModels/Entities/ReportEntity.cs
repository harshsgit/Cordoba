using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class ReportEntity
    {
       public int? GroupById { get; set; }
       public int? StatusId { get; set; }
       public Nullable<DateTime> DateStart { get; set; }
       public Nullable<DateTime> DateEnd { get; set; }
       public int? No_Of_Orders { get; set; }
       public int TotalRecords { get; set; }

       public int? No_Of_Products { get; set; }
       public decimal? Total { get; set; }
       public int? Tax { get; set; }


       //Transaction
       public string firstname { get; set; }
       public string lastname { get; set; }
       public string email { get; set; }
       public string store { get; set; }
       public int adjustment { get; set; }
       public int points { get; set; }
       public string comments { get; set; }
       public DateTime Date { get; set; }
       public string type_of_points { get; set; }


       public string comment { get; set; }
       public string model { get; set; }
       public string product_name { get; set; }
       public int quantity { get; set; }


       public string category { get; set; }

       public string status { get; set; }
       public string activated { get; set; }

       public DateTime Date_Added { get; set; }

       //customer report list
       public string StoreName { get; set; }
       public int? Balance { get; set; }

       public DateTime timestamp { get; set; }
    }
}
