using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System.Linq;

namespace Application.DAL.DTO
{
    public class CartOfferDTO : EntityDTO, IMapFrom<CartOffer>
    {
        public double PriceForOneProduct { get; set; }
        public string Title { get; set; }
        public ProductImageDTO Image { get; set; }
        public int ProductsCount { get; set; }
        public long CartId { get; set; }
        public long OfferId { get; set; }
        public OfferState OfferState { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CartOffer, CartOfferDTO>()
                .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.Offer.Id))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Offer.Images.Where(x => x.IsMainProductImage).SingleOrDefault()))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Offer.Title))
                .ForMember(dest => dest.PriceForOneProduct, opt => opt.MapFrom(src => src.Offer.PriceForOneProduct))
                .ForMember(dest => dest.OfferState, opt => opt.MapFrom(src => src.Offer.State))
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Cart.Id));
        }
    }
}
