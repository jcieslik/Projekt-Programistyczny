using System;

namespace Application.DAL.DTO.CommandDTOs.Update
{
    public class UpdateProductRateDTO
    {
        public Guid Id { get; set; }
        public double? Value { get; set; }
        public bool? IsHidden { get; set; }
    }
}
