using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateBidDTO
    {
        public double Value { get; set; }
        public long BidderId { get; set; }
        public long OfferId { get; set; }
    }
}
