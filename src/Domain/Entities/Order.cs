using Domain.Common;
using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Order : AuditableEntity
    {
        public User Customer { get; set; }
        public Offer Offer { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        public DateTime? PaymentDate { get; set; }
    }
}
