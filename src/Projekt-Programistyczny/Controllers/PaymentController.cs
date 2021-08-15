using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using Application.DAL.DTO;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.DAL.DTO.CommandDTOs.Update;
using System.Threading.Tasks;
using Domain.Enums;

namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "CustomerOnly")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUserConfig userConfig;
        private readonly IOrderService orderService;

        public PaymentController(IUserConfig userConfig, IOrderService orderService) 
        {
            this.userConfig = userConfig;
            this.orderService = orderService;
        }

        [HttpPost]
        [Route("MakePayment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> MakePayment([FromBody] PaymentRequestDTO paymentRequest, [FromQuery] long orderId)
        {
            StripeConfiguration.ApiKey = userConfig.StripeSecret;

            var options = new ChargeCreateOptions
            {
                Amount = paymentRequest.Amount,
                Source = paymentRequest.TokenId,
                Description = paymentRequest.Description,
                Currency = "pln",
            };
            var service = new ChargeService();

            try
            {
                service.Create(options);
                var order = new UpdateOrderDTO()
                {
                    Id = orderId,
                    OrderStatus = OrderStatus.Paid,
                    PaymentDate = DateTime.Now
                };
                await orderService.UpdateOrder(order);
                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
