﻿using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Xml;

namespace CanddelsBackEnd.Models
{
    public class Product : BaseEntity
    {  
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }   
        public string? Benfits { get; set; }   
        

        [DefaultValue(false)] 
        public bool IsBestSeller{ get; set; }

        [DefaultValue(false)]
        public bool IsDeleted {  get; set; } 
        
        [DefaultValue(false)]
        public bool IsDailyOffer {  get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        
        public string? CalltoAction { get; set; }
    

        public virtual ICollection<ProductVariant> productVariants { get; set; } = new List<ProductVariant>();

        public decimal? DiscountPercentage { get; set; }


        public int CategoryId { get; set; }
        public int ScentId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Scent Scent { get; set; }

    }
}
