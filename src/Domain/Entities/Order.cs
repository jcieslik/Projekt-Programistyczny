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
        public double FullPrice { get; set; }
        public Offer Offer { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public double? DeliveryFullPrice { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string DestinationCity { get; set; }
        public string DestinationStreet { get; set; }
        public string DestinationPostCode { get; set; }

        public Comment Comment { get; set; }
        public long? CommentId { get; set; }
    }
}
