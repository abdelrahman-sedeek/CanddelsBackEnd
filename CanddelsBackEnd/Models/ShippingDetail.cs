﻿namespace CanddelsBackEnd.Models
{
    public class ShippingDetail
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }

}
