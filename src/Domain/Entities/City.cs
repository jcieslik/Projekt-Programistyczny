using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class City : AuditableEntity
    {
        public string Name { get; set; }

        public ICollection<Offer> Offers { get; set; }
    }
}
