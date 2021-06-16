using System;

namespace Application.DAL.DTO.CommandDTOs.Update
{
    public class UpdateOfferDTO
    {
        public long Id { get; set; }
        public int? ProductCount { get; set; }
        public double? PriceForOneProduct { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? CityId { get; set; }
        public long? ProvinceId { get; set; }
        public int? ProductState { get; set; }
        public int? State { get; set; }
        public int? OfferType { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
    }
}
