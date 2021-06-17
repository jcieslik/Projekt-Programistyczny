using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateOrderDTO
    {
        public Guid CustomerId { get; set; }
        public Guid OfferId { get; set; }
        public int OrderStatus { get; set; }
    }
}
