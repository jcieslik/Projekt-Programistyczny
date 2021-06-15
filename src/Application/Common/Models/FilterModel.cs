﻿using System;
using System.Collections.Generic;

namespace Application.Common.Models
{
    public class FilterModel
    {
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public ICollection<long> CitiesIds { get; set; }
        public ICollection<long> ProvincesIds { get; set; }
        public ICollection<long> BrandsIds { get; set; }
        public long CategoryId { get; set; }
        public int OfferType { get; set; }
        public int ProductState { get; set; }
        public int OfferState { get; set; }
    }
}
