using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateProductImageDTO
    {
        public long OfferId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public bool IsMainProductImage { get; set; }
    }
}
