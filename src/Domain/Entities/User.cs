using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public UserRole Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Offer> SoldOffers { get; set; }
        public ICollection<Offer> Acquisitions { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ProductRate> Rates { get; set; }
        public bool IsActive { get; set; }

    }
}
