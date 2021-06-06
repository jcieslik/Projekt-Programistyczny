using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class ProductImageDTO : EntityDTO, IMapFrom<ProductImage>
    {
        public OfferDTO Offer { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public bool IsMainProductImage { get; set; }
    }
}
