using System;

namespace Application.DAL.DTO.CommandDTOs.Update
{
    public class UpdateOrderDTO
    {
        public long Id { get; set; }
        public int? OrderStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationStreet { get; set; }
        public string DestinationPostCode { get; set; }
    }
}
