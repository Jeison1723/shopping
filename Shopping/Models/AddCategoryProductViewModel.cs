﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class AddCategoryProductViewModel
    {
        public int ProductId { get; set; }
        [Display(Name ="Categoria")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoria.")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
