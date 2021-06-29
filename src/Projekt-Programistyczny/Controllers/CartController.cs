﻿using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Add;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt_Programistyczny.Extensions;
using Projekt_Programistyczny.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        public CartController(ICartService cartService, IUserService userService)
        {
            _cartService = cartService;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetOffersFromCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OfferWithBaseDataDTO>>> GetOffersFromCart([FromQuery] long userId)
        {
            try
            {
                var offers = await _cartService.GetOffersFromCartAsync(userId);
                return Ok(offers);
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
        public async Task<IActionResult> AddOfferToCart([FromQuery] long offerId)
        {
            try
            {
                var userId = HttpContext.User.GetUserId();
                await _cartService.AddOfferToCartAsync(offerId, userId);
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
                var userId = HttpContext.User.GetUserId();
                await _cartService.RemoveOfferFromCartAsync(offerId, userId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
