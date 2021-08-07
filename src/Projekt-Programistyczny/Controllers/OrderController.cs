using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt_Programistyczny.Extensions;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
        [Route("CreateOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(dto);
                return Ok(order);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPatch]
        [Route("ChangeStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDTO>> ChangeStatus([FromBody] UpdateOrderDTO dto)
        {
            try
            {
                var order = await _orderService.UpdateOrder(dto);
                return Ok(order);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
        [Route("GetOrdersByCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<OrderDTO>>> GetOrdersByCustomer([FromBody] PaginationProperties pagination)
        {
            var orders = await _orderService.GetPaginatedOrdersByCustomer(HttpContext.User.GetUserId(), pagination);
            return Ok(orders);
        }

        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
        [Route("GetOrdersBySeller")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<OrderDTO>>> GetOrdersBySeller([FromBody] PaginationProperties pagination)
        {
            var orders = await _orderService.GetPaginatedOrdersBySeller(HttpContext.User.GetUserId(), pagination);
            return Ok(orders);
        }
    }
}
