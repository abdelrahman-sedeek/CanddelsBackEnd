using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CanddelsBackEnd.Models
{
    
        public class User
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public string PhoneNumber { get; set; }
            public string Role { get; set; } // Admin, Customer
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }

            public ICollection<Order> Orders { get; set; }
            public Cart Cart { get; set; }
           
        }


}
