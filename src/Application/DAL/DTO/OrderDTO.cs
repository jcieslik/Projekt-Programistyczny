using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.DAL.DTO
{
    public class OrderDTO : EntityDTO, IMapFrom<Order>
    {
        public UserDTO Customer { get; set; }
        public OfferDTO Offer { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
