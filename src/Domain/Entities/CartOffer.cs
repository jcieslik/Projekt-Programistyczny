using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CartOffer
    {
        [Key, Column(Order = 1)]
        public long CartId { get; set; }
        //[Key, Column(Order = 2)]
        public long OfferId { get; set; }
    }
}
