using Domain.Common;
using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Offer : AuditableEntity
    {
        public int ProductCount { get; set; }
        public double PriceForOneProduct { get; set; }
        public OfferState State { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid SellerId { get; set; }
        public User Seller { get; set; }
        public Guid? CustomerId { get; set; }
        public User Customer { get; set; }

        public Guid CityId { get; set; }
        public City City { get; set; }
        public Guid ProvinceId { get; set; }
        public Province Province { get; set; }
    }
}
