using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateBidDTO
    {
        public double Value { get; set; }
        public Guid BidderId { get; set; }
        public Guid OfferId { get; set; }
    }
}
