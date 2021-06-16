using Application.Common.Dto;
using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateProductRateDTO
    {
        public long OfferId { get; set; }
        public long UserId { get; set; }
        public double Value { get; set; }
    }
}
