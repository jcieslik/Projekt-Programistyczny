using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateOrderDTO
    {
        public long CustomerId { get; set; }
        public long OfferId { get; set; }
        public int OrderStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
