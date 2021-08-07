using Domain.Enums;
using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateOrderDTO
    {
        public long CustomerId { get; set; }
        public long OfferWithDeliveryId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int ProductCount { get; set; }
        public double FullPrice { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationStreet { get; set; }
        public string DestinationPostCode { get; set; }
        public long? CartOfferId { get; set; }
    }
}
