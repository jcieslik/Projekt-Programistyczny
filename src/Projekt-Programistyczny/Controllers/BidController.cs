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
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BidDTO>> GetBidById([FromRoute] Guid id)
        {
            var bid = await _bidService.GetBidByIdAsync(id);
            if(bid == null)
            {
                return NotFound();
            }
            return Ok(bid);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("Offer/{id}")]
        public async Task<ActionResult<IEnumerable<BidDTO>>> GetBidsFromOffer([FromRoute] Guid id, [FromQuery] bool onlyNotHidden = true)
        {
            try
            {
                var bids = await _bidService.GetBidsFromOfferAsync(id, onlyNotHidden);
                return Ok(bids);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("User/{id}")]
        public async Task<ActionResult<IEnumerable<BidDTO>>> GetBidsFromUser([FromRoute] Guid id, [FromQuery] bool onlyNotHidden = true)
        {
            try
            {
                var bids = await _bidService.GetBidsFromUserAsync(id, onlyNotHidden);
                return Ok(bids);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BidDTO>> Create([FromBody] CreateBidDTO dto)
        {
            try
            {
                var bid = await _bidService.CreateBidAsync(dto);
                return Ok(bid);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BidDTO>> Update([FromBody] UpdateBidDTO dto)
        {
            try
            {
                var bid = await _bidService.UpdateBidAsync(dto);
                return Ok(bid);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
