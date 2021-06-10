using Application.Common.Dto;
using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateProductRateDTO
    {
        public Guid OfferId { get; set; }
        public Guid UserId { get; set; }
        public double Value { get; set; }
    }
}
