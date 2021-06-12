using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Application.DAL.DTO
{
    public class OfferDTO : EntityDTO, IMapFrom<Offer>
    {
        public int ProductCount { get; set; }
        public double PriceForOneProduct { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public UserDTO Seller { get; set; }
        public ICollection<OrderDTO> Orders { get; set; }
        public CityDTO City { get; set; }
        public ProvinceDTO Province { get; set; }
        public ProductState ProductState { get; set; }
        public OfferState State { get; set; }
        public OfferType OfferType { get; set; }

        public ProductCategoryDTO Category { get; set; }
        public BrandDTO Brand { get; set; }

        public ICollection<ProductImageDTO> Images { get; set; }
        public ICollection<ProductRateDTO> Rates { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<BidDTO> Bids { get; set; }
    }
}
