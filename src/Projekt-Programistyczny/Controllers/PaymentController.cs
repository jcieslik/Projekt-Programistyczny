using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DAL.DTO;
using Application.Common.Interfaces;

namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUserConfig userConfig;

        public PaymentController(IUserConfig userConfig) 
        {
            this.userConfig = userConfig;
        }

        [HttpPost]
        [Route("MakePayment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> MakePayment([FromBody] PaymentRequestDTO paymentRequest)
        {
            StripeConfiguration.ApiKey = userConfig.StripeSecret;

            var options = new ChargeCreateOptions
            {
                Amount = paymentRequest.amount,
                Source = paymentRequest.tokenId,
                Description = paymentRequest.description,
                Currency = "pln",
            };
            var service = new ChargeService();

            try
            {
                service.Create(options);
                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
