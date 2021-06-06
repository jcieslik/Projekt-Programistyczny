using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Offer : AuditableAndAbleToBeHiddenEntity
    {
        [Required]
        public int ProductCount { get; set; }

        [Required]
        public double PriceForOneProduct { get; set; }

        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public User Seller { get; set; }
        public ICollection<Order> Orders { get; set; }

        public City City { get; set; }
        public Province Province { get; set; }

        [Required]
        public ProductState ProductState { get; set; }

        [Required]
        public OfferState State { get; set; }

        public ProductCategory Category { get; set; }
        public Brand Brand { get; set; }

        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductRate> Rates { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public ICollection<Wish> Wishes { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
