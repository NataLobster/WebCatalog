using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCatalog.Models
{
    public class CatalogProd
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        public int IdCategory { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string ProdName { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string DescriptionProd { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Currency, ErrorMessage ="это деньги!")]
        public decimal Price { get; set; }
        public string Remark { get; set; }
        public string SpecialRemark { get; set; }
    }
}
