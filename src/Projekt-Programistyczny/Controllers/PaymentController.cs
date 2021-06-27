using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DAL.DTO;

namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public PaymentController() {}

        [HttpPost]
        [Route("MakePayment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> MakePayment([FromBody] PaymentRequestDTO paymentRequest)
        {
            StripeConfiguration.ApiKey = "sk_test_51J6t0pFBQwYWjNW6dpz7HqgexrbBGzvpDT9yeWARx0KUJNhiTX76XF8WZ9pGcamTDuZl4NkSuzr3Ms0QEkE7e1tc00s6Wr0tZd";

            var options = new ChargeCreateOptions
            {
                Amount = paymentRequest.amount,
                Currency = "pln",
                Source = paymentRequest.tokenId,
                Description = "My First Test Charge (created for API docs)",
            };
            var service = new ChargeService();
            service.Create(options);

            return Ok();
        }
    }
}
