using Domain.Enums;
using System.Collections.Generic;

namespace Application.Common.Models
{
    public class FilterModel
    {
        public string SearchText { get; set; }
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
    }
}
