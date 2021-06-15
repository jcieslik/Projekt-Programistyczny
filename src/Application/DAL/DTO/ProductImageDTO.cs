using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;

namespace Application.DAL.DTO
{
    public class ProductImageDTO : EntityDTO, IMapFrom<ProductImage>
    {
        public long OfferId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public bool IsMainProductImage { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductImage, ProductImageDTO>()
                .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.Offer.Id));
        }
    }
}
