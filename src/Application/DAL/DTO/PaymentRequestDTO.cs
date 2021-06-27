using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.DTO
{
    public class PaymentRequestDTO
    {
        public string tokenId { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
    }
}
