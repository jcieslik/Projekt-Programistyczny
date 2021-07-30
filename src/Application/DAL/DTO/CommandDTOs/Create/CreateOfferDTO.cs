using Application.DAL.DTO.CommandDTOs.AddOrRemove;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateOfferDTO
    {
        public int ProductCount { get; set; }
        public double PriceForOneProduct { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long SellerId { get; set; }
        public string City { get; set; }
        public long ProvinceId { get; set; }
        public ProductState ProductState { get; set; }
        public OfferState State { get; set; }
        public OfferType OfferType { get; set; }
        public long CategoryId { get; set; }
        public string Brand { get; set; }
        public IEnumerable<ProductImageDTO> Images { get; set; }
        public IEnumerable<AddDeliveryMethodWihOfferRelationDTO> DeliveryMethods { get; set; }
    }
}
