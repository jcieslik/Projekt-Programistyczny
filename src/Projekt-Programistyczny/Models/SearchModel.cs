using Domain.Enums;
using System.Collections.Generic;

namespace Projekt_Programistyczny.Models
{
    public class SearchModel
    {
        public long? SellerId { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public ICollection<string> Cities { get; set; }
        public ICollection<long> ProvincesIds { get; set; }
        public ICollection<string> Brands { get; set; }
        public long? CategoryId { get; set; }
        public OfferType? OfferType { get; set; }
        public ProductState? ProductState { get; set; }
        public OfferState? OfferState { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string SearchText { get; set; }
    }
}
