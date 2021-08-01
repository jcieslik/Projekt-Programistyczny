using Domain.Common;
using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Order : AuditableEntity
    {
        public User Customer { get; set; }
        public int ProductCount { get; set; }
        public OfferAndDeliveryMethod OfferWithDelivery { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        public DateTime? PaymentDate { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationStreet { get; set; }
        public string DestinationPostCode { get; set; }
    }
}
