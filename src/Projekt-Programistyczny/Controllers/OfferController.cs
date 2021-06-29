using Application.Common.Exceptions;
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
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly IUserService _userService;

        public OfferController(IOfferService offerService, IUserService userService)
        {
            _offerService = offerService;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetOfferById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OfferDTO>> GetOfferById([FromQuery] long id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            if(offer == null)
            {
                return NotFound();
            }
            return Ok(offer);
        }

        [HttpGet]
        [Route("GetAllOffers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OfferWithBaseDataDTO>>> GetAllOffers()
        {
            var offers = await _offerService.GetOffersAsync();
            return Ok(offers);
        }

        [HttpGet]
        [Route("GetOffersFromUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OfferWithBaseDataDTO>>> GetOffersFromUser([FromQuery] long id)
        {
            try 
            {
                var offers = await _offerService.GetOffersFromUserAsync(id);
                return Ok(offers);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetFromCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OfferWithBaseDataDTO>>> GetOffersFromCart([FromQuery] long id)
        {
            try
            {
                var offers = await _offerService.GetOffersFromCartAsync(id);
                return Ok(offers);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetFromUserWishes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<OfferWithBaseDataDTO>>> GetOffersFromActiveUserWishes(
            [FromQuery] long userId,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 15,
            [FromQuery] string orderBy = "creation")
        {
            var properties = new PaginationProperties
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                OrderBy = orderBy
            };
            var offers = await _offerService.GetPaginatedOffersFromUserActiveWishesAsync(userId, properties);

            return Ok(offers);
        }

        [HttpPost]
        [Route("GetOffers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<OfferWithBaseDataDTO>>> GetOffers([FromBody] SearchModel searchModel)
        {
            var filterModel = new FilterModel
            {
                Brands = searchModel.Brands,
                OfferState = searchModel.OfferState,
                OfferType = searchModel.OfferType,
                ProductState = searchModel.ProductState,
                CategoryId = searchModel.CategoryId,
                Cities = searchModel.Cities,
                ProvincesIds = searchModel.ProvincesIds,
                MaxPrice = searchModel.MaxPrice,
                MinPrice = searchModel.MinPrice
            };

            var paginationProperties = new PaginationProperties
            {
                PageSize = searchModel.PageSize,
                OrderBy = searchModel.OrderBy,
                PageIndex = searchModel.PageIndex
            };

            var offers = await _offerService.GetPaginatedOffersAsync(filterModel, paginationProperties);
            return Ok(offers);
        }

        [HttpPost]
        [Route("CreateOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OfferDTO>> CreateOffer([FromBody] CreateOfferDTO dto)
        {
            try
            {
                var offer = await _offerService.CreateOfferAsync(dto);
                return Ok(offer);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("AddToCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddOfferToCart([FromQuery] long offerId)
        {
            var id = HttpContext.User.GetUserId();
            var user = await _userService.GetUserById(id);

            try
            {
                AddOrRemoveOfferToCartDTO parameters = new() { CartId = user.CartId, OfferId = offerId };
                await _offerService.AddOfferToCartAsync(parameters);
                return Ok();
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("RemoveFromCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveOfferFromCart([FromQuery] long id)
        {
            try
            {
                //await _offerService.RemoveOfferFromCartAsync(dto);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OfferDTO>> UpdateOffer([FromBody] UpdateOfferDTO dto)
        {
            try
            {
                var offer = await _offerService.UpdateOfferAsync(dto);
                return Ok(offer);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
