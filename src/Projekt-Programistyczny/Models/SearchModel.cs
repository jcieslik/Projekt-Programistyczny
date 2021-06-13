using System;
using System.Collections.Generic;

namespace Projekt_Programistyczny.Models
{
    public class SearchModel
    {
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public ICollection<Guid> CitiesIds { get; set; }
        public ICollection<Guid> ProvincesIds { get; set; }
        public ICollection<Guid> BrandsIds { get; set; }
        public Guid CategoryId { get; set; }
        public int OfferType { get; set; }
        public int ProductState { get; set; }
        public int OfferState { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
    }
}
