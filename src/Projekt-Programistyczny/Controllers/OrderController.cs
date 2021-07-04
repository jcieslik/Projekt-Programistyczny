using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
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


    }
}
