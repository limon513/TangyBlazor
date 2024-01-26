﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool ShopFavourtie { get; set; }
        public bool CustomerFavourite { get; set; }
        [Required(ErrorMessage ="Color de beda!")]
        public string Color { get; set; }
        [Required(ErrorMessage ="Image dibo kigu halarhala")]
        public string ImageUrl { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Please select a category...")]
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
