using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Offer : AuditableAndAbleToBeHiddenEntity
    {
        public int ProductCount { get; set; }
        public double PriceForOneProduct { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public User Seller { get; set; }
        public User Customer { get; set; }

        public City City { get; set; }
        public Province Province { get; set; }

        public ProductState ProductState { get; set; }
        public OfferState State { get; set; }

        public ProductCategory Category { get; set; }
        public Brand Brand { get; set; }

        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductRate> Rates { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Bid> Bids { get; set; }
    }
}
