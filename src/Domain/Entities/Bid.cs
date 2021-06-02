using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bid : AuditableEntity
    {
        public double Value { get; set; }
        public User Bidder { get; set; }
        public Offer Offer { get; set; }
    }
}
