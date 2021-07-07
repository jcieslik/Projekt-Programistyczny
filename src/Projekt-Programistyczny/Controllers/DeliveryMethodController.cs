using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.AddOrRemove;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using Microsoft.AspNetCore.Authorization;
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
    public class DeliveryMethodController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryMethodController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet]
        [Route("GetDeliveryMethodById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<DeliveryDTO>>> GetDeliveryMethodById([FromQuery] long id)
        {
            var method = await _deliveryService.GetDeliveryMethodByIdAsync(id);
            if (method == null)
            {
                return NotFound();
            }
            return Ok(method);
        }

        [HttpGet]
        [Route("GetDeliveryMethods")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DeliveryDTO>>> GetDeliveryMethods()
        {
            var methods = await _deliveryService.GetDeliveryMethodsAsync();
            return Ok(methods);
        }

        [HttpGet]
        [Route("GetDeliveryMethodsFromOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<DeliveryDTO>>> GetDeliveryMethodsFromOffer([FromQuery] long offerId)
        {
            try
            {
                var methods = await _deliveryService.GetDeliveryMethodsFromOfferAsync(offerId);
                return Ok(methods);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [Route("CreateDeliveryMethod")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeliveryDTO>> CreateDeliveryMethod([FromBody] CreateDeliveryMethodDTO dto)
        {
            var method = await _deliveryService.CreateDeliveryMethod(dto);
            return Ok(method);
        }

        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
        [Route("AddDeliveryMethodToOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddDeliveryMethodToOffer([FromBody] AddDeliveryMethodWihOfferRelationDTO dto)
        {
            try
            {
                await _deliveryService.AddOfferAndDeliveryMethodRelation(dto);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (RelationAlreadyExistException)
            {
                return Conflict();
            }
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateDeliveryMethod")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeliveryDTO>> UpdateDeliveryMethod([FromBody] UpdateDeliveryMethodDTO dto)
        {
            try
            {
                var method = await _deliveryService.UpdateDeliveryMethod(dto);
                return Ok(method);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminOnly")]
        [Route("DeleteDeliveryMethod/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDeliveryMethod([FromRoute] long id)
        {
            try
            {
                await _deliveryService.DeleteDeliveryMethodAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("RemoveDeliveryMethodFromOffer/{offerId}/{methodId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveDeliveryMethodFromOffer([FromRoute] long offerId, [FromRoute] long methodId)
        {
            try
            {
                await _deliveryService.RemoveOfferAndDeliveryMethodRelation(offerId, methodId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
