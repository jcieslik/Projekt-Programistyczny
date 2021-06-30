using System;
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
        public int OfferType { get; set; }
        public int ProductState { get; set; }
        public int OfferState { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
    }
}
