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
        public Guid SellerId { get; set; }
        public Guid CityId { get; set; }
        public Guid ProvinceId { get; set; }
        public int ProductState { get; set; }
        public int State { get; set; }
        public int OfferType { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public IEnumerable<ProductImageDTO> Images { get; set; }
    }
}
