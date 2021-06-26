using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateCommentDTO
    {
        public long OfferId { get; set; }
        public long CustomerId { get; set; }
        public long SellerId { get; set; }
        public string Content { get; set; }
        public double RateValue { get; set; }
    }
}
