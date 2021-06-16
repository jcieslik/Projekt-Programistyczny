using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateProductImageDTO
    {
        public long OfferId { get; set; }
        public string ImageData { get; set; }
        public bool IsMainProductImage { get; set; }
    }
}
