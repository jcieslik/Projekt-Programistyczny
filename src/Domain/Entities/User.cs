using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        public UserRole Role { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public Province Province { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string PostCode { get; set; }

        public string BankAccountNumber { get; set; }

        public ICollection<Offer> SoldOffers { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public ICollection<MessageTransmission> Messages { get; set; }
        public ICollection<MessageUser> MessagesReceived { get; set; }
        public ICollection<Wish> Wishes { get; set; }
        public Cart Cart { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string BanInfo { get; set; }
    }
}
