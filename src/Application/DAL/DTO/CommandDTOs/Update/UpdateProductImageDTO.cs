using System;

namespace Application.DAL.DTO.CommandDTOs.Update
{
    public class UpdateProductImageDTO
    {
        public long Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public bool? IsMainProductImage { get; set; }
        public bool? IsHidden { get; set; }
    }
}
