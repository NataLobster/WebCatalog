using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCatalog.Models
{
    public class CatalogProd
    {
        public int Id { get; set; }
        public int IdCategory { get; set; }
        public string ProdName { get; set; }
        public string DescriptionProd { get; set; }
        public decimal Price { get; set; }
        public string Remark { get; set; }
        public string SpecialRemark { get; set; }
    }
}
