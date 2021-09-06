using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt_Programistyczny.Extensions;
using Projekt_Programistyczny.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly IMessageService _messageService;

        public OfferController(IOfferService offerService, IMessageService messageService)
        {
            _offerService = offerService;
            _messageService = messageService;
        }

        [HttpGet]
        [Route("GetOfferById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OfferDTO>> GetOfferById([FromQuery] long id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            if (offer == null)
            {
                return NotFound();
            }
            return Ok(offer);
        }

        [HttpGet]
        [Authorize]
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

        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
        [Route("GetOffersFromActiveUserWishes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<OfferWithBaseDataDTO>>> GetOffersFromActiveUserWishes([FromBody] SearchModel searchModel)
        {
            var filterModel = new FilterModel
            {
                SearchText = searchModel.SearchText,
                CategoryId = searchModel.CategoryId,
            };

            var properties = new PaginationProperties
            {
                PageSize = searchModel.PageSize,
                PageIndex = searchModel.PageIndex,
                OrderBy = searchModel.OrderBy
            };
            var offers = await _offerService.GetPaginatedOffersFromUserActiveWishesAsync(HttpContext.User.GetUserId(), filterModel, properties);

            return Ok(offers);
        }

        [HttpPost]
        [Route("GetOffers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<OfferWithBaseDataDTO>>> GetOffers([FromBody] SearchModel searchModel, [FromQuery] OfferState state = OfferState.All)
        {
            var filterModel = new FilterModel
            {
                SearchText = searchModel.SearchText,
                SellerId = searchModel.SellerId,
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

            var offers = await _offerService.GetPaginatedOffersAsync(filterModel, paginationProperties, state);
            return Ok(offers);
        }

        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
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
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
        [Route("GetUserActiveBidOffers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaginatedList<OfferWithBaseDataDTO>>> GetUserActiveBidOffers([FromBody] PaginationProperties paginationProperties)
        {
            try
            {
                var result = await _offerService.GetUserActiveBidOffers(HttpContext.User.GetUserId(), paginationProperties);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
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

        [HttpGet]
        [Route("DecrementOfferProductCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DecrementOfferProductCount([FromBody] long offerId, int count)
        {
            try
            {
                if (count > 0)
                {
                    var offer = await _offerService.GetOfferByIdAsync(offerId);
                    return Ok(offer.ProductCount > 1);
                }
                return Ok(false);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        [Route("BanOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> BanOffer([FromBody] BanDto banDto)
        {
            try
            {
                var offerDto = new UpdateOfferDTO();
                offerDto.Id = banDto.Id;
                offerDto.State = (int?)OfferState.Banned;
                await _offerService.UpdateOfferAsync(offerDto);
                
                var offer = await _offerService.GetOfferByIdAsync(banDto.Id);
                var messageDto = new CreateMessageDTO();
                messageDto.Content = "Twoje ogłoszenie zostało zbanowane!<br>" + "Tytuł ogłoszenia: " + offer.Title + "<br>"
                    + "Powód blokady: " + banDto.BanInfo + "<br><br>Prosimy spróbować ponownie po ponownym zapoznaniu się z regulaminem.<br><br>Życzymy miłego dnia!";
                messageDto.SenderId = 1;
                messageDto.Topic = "Blokada ogłoszenia";
                messageDto.SendDate = DateTime.Now;
                messageDto.RecipientsIds = new List<long>() { offer.Seller.Id };
                await _messageService.CreateMessageAsync(messageDto);
                return Ok(true);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        [Route("UnbanOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> UnbanOffer([FromBody] long offerId)
        {
            try
            {
                var offerDto = new UpdateOfferDTO();
                offerDto.Id = offerId;
                offerDto.State = (int?)OfferState.Awaiting;
                await _offerService.UpdateOfferAsync(offerDto);
                return Ok(true);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
