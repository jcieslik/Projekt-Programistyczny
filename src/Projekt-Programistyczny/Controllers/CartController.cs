using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt_Programistyczny.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "CustomerOnly")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Route("GetOffersFromCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CartOfferDTO>>> GetOffersFromCart()
        {
            try
            {
                var offers = await _cartService.GetOffersFromCartAsync(HttpContext.User.GetUserId());
                return Ok(offers);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<long>> Create()
        {
            try
            {
                var id = await _cartService.Create(HttpContext.User.GetUserId());
                return Ok(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("AddOfferToCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddOfferToCart([FromQuery] long offerId, [FromQuery] int amount)
        {
            try
            {
                await _cartService.AddOfferToCartAsync(offerId, amount, HttpContext.User.GetUserId());
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("RemoveOfferFromCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveOfferFromCart([FromQuery] long offerId)
        {
            try
            {
                await _cartService.RemoveOfferFromCartAsync(offerId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateProductCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProductCount([FromQuery] long offerId, [FromQuery] int productCount)
        {
            try
            {
                var userId = HttpContext.User.GetUserId();
                await _cartService.UpdateProductCountAsync(HttpContext.User.GetUserId(), offerId, productCount);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetNumberOfOffersInCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNumberOfOffersInCart([FromQuery] long offerId, [FromQuery] int productCount)
        {
            try
            {
                var userId = HttpContext.User.GetUserId();
                await _cartService.UpdateProductCountAsync(HttpContext.User.GetUserId(), offerId, productCount);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
