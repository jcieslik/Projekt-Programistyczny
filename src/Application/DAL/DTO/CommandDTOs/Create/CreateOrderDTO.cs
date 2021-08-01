using Domain.Enums;
using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateOrderDTO
    {
        public long CustomerId { get; set; }
        public long OfferAndDeliveryId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int ProductCount { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationStreet { get; set; }
        public string DestinationPostCode { get; set; }
    }
}
