using Domain.Common;
using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Offer : AuditableAndAbleToBeHiddenEntity
    {
        public int ProductCount { get; set; }
        public double PriceForOneProduct { get; set; }
        public OfferState State { get; set; }

        public Product Product { get; set; }

        public User Seller { get; set; }
        public User Customer { get; set; }

        public City City { get; set; }
        public Province Province { get; set; }
    }
}
