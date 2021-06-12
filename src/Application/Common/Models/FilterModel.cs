using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class FilterModel
    {
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public ICollection<Guid> CitiesIds { get; set; }
        public ICollection<Guid> ProvincesIds { get; set; }
        public ICollection<Guid> BrandsIds { get; set; }
        public Guid CategoryId { get; set; }
    }
}
