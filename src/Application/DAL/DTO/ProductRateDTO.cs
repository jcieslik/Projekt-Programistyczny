using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class ProductRateDTO : EntityDTO, IMapFrom<ProductRate>
    {
        public double Value { get; set; }
        public OfferDTO Offer { get; set; }
        public UserDTO Customer { get; set; }
    }
}
