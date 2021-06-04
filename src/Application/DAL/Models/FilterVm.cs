using Application.DAL.DTO;
using Domain.Enums;
using System.Collections.Generic;

namespace Application.DAL.Models
{
    public class FilterVm
    {
        public IList<CityDTO> Cities { get; set; }
        public IList<ProvinceDTO> Provinces { get; set; }
        public IList<BrandDTO> Brands { get; set; }
        public IList<OfferType> OfferTypes { get; set; }
        public IList<ProductState> ProductStates { get; set; }
    }
}
