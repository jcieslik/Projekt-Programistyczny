using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.DTO
{
    public class PaymentRequestDTO
    {
        public string TokenId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
