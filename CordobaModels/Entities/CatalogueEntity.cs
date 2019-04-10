using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class CatalogueEntity
    {
        public int catalogue_Id { get; set; }
        public string Name { get; set; }
        public bool? IsSelected { get; set; }
    }
}
