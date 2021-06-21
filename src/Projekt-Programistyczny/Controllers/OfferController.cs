using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Add;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt_Programistyczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
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
        public async Task<ActionResult<IEnumerable<OfferWithBaseDataDTO>>> GetOffersFromCaer([FromQuery] long cartId)
        {
            try
            {
                var offers = await _offerService.GetOffersFromCartAsync(cartId);
                return Ok(offers);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("GetOffers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<OfferWithBaseDataDTO>>> GetOffers([FromBody] SearchModel searchModel)
        {
            var filterModel = new FilterModel
            {
                BrandsIds = searchModel.BrandsIds,
                OfferState = searchModel.OfferState,
                OfferType = searchModel.OfferType,
                ProductState = searchModel.ProductState,
                CategoryId = searchModel.CategoryId,
                CitiesIds = searchModel.CitiesIds,
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
        public async Task<ActionResult<OfferDTO>> Create([FromBody] CreateOfferDTO dto)
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
        public async Task<IActionResult> AddOfferToCart([FromBody] AddOrRemoveOfferToCartDTO dto)
        {
            try
            {
                await _offerService.AddOfferToCartAsync(dto);
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
        public async Task<IActionResult> RemoveOfferFromCart([FromBody] AddOrRemoveOfferToCartDTO dto)
        {
            try
            {
                await _offerService.RemoveOfferFromCartAsync(dto);
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
        public async Task<ActionResult<OfferDTO>> Update([FromBody] UpdateOfferDTO dto)
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
