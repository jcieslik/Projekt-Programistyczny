using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class BidDTO : EntityDTO, IMapFrom<Bid>
    {
        public double Value { get; set; }
        public UserDTO Bidder { get; set; }
        public OfferDTO Offer { get; set; }
    }
}
