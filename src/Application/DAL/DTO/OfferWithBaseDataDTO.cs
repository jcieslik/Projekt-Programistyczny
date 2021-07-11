using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Linq;

namespace Application.DAL.DTO
{
    public class OfferWithBaseDataDTO : IMapFrom<Offer>
    {
        public long Id { get; set; }
        public double PriceForOneProduct { get; set; }
        public int ProductCount { get; set; }
        public string Title { get; set; }
        public OfferType OfferType { get; set; }
        public ProductState ProductState { get; set; }
        public ProductImageDTO Image { get; set; }
        public BidDTO BestBid { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Offer, OfferWithBaseDataDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.Where(x => x.IsMainProductImage).SingleOrDefault()))
                .ForMember(dest => dest.BestBid, opt => opt.MapFrom(src => src.Bids.OrderByDescending(x => x.Value).FirstOrDefault()));
        }
    }
}
