using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.DTO
{
    public class OfferDeliveryDTO : EntityDTO, IMapFrom<OfferAndDeliveryMethod>
    {
        public long OfferId { get; set; }
        public double DeliveryFullPrice { get; set; }
        public string DeliveryMethodName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OfferAndDeliveryMethod, OfferDeliveryDTO>()
                .ForMember(dest => dest.DeliveryMethodName, opt => opt.MapFrom(src => src.DeliveryMethod.Name));
        }
    }
}
