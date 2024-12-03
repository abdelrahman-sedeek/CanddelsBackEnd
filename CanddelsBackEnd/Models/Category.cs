﻿namespace CanddelsBackEnd.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Product> products { get; set; } = new List<Product>();

        public string? ImageUrl { get; set; }
    }
}
