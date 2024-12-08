using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Xml;

namespace CanddelsBackEnd.Models
{
    public class Product : BaseEntity
    {  
        public string Name { get; set; }
        public string Description { get; set; }
        public string Scent { get; set; }

        [DefaultValue(false)] 
        public bool IsBestSeller{ get; set; }

        [DefaultValue(false)]
        public bool IsDeleted {  get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        
        public string Benefits { get; set; }
        public string Features { get; set; }
        public string CalltoAction { get; set; }
    
        public int ProductVariantId {  get; set; }
        public virtual ICollection<ProductVariant> productVariants { get; set; } = new List<ProductVariant>();

        public int? DiscountId { get; set; }

        public virtual Discount? Discount { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
