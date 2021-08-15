using Domain.Enums;
using System;

namespace Application.DAL.DTO.CommandDTOs.Update
{
    public class UpdateOrderDTO
    {
        public long Id { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public double? FullPrice { get; set; }
        public long? OfferWithDeliveryId { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationStreet { get; set; }
        public string DestinationPostCode { get; set; }
    }
}
