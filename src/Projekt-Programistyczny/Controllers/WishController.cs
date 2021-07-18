using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
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
    public class WishController : ControllerBase
    {
        private readonly IWishService _wishService;

        public WishController(IWishService wishService)
        {
            _wishService = wishService;
        }

        [HttpPost]
        [Route("CreateWish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WishDTO>> CreateWish(CreateWishDto dto)
        {
            try
            {
                var wish = await _wishService.CreateWishAsync(dto);
                return Ok(wish);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (RelationAlreadyExistException)
            {
                return Conflict();
            }
        }

        [HttpPatch]
        [Route("HideWish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> HideWish([FromQuery] long id)
        {
            try
            {
                await _wishService.HideWish(id, HttpContext.User.GetUserId());
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("CheckForUserWish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckForUserWish([FromQuery] long id)
        {
            return Ok(await _wishService.CheckForUserWish(id, HttpContext.User.GetUserId()));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromQuery] long offerId, [FromQuery] long userId)
        {
            try
            {
                await _wishService.DeleteAsync(offerId, userId);
                return Ok();
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
