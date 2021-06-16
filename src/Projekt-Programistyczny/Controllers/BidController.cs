﻿using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.DAL.DTO;
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
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        [HttpGet]
        [Route("GetBidById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BidDTO>> GetBidById([FromQuery] long id)
        {
            var bid = await _bidService.GetBidByIdAsync(id);
            if(bid == null)
            {
                return NotFound();
            }
            return Ok(bid);
        }

        [HttpGet]
        [Route("GetBidsFromOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BidDTO>>> GetBidsFromOffer([FromQuery] long id, [FromQuery] bool onlyNotHidden = true)
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
        [Route("GetBidsFromUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BidDTO>>> GetBidsFromUser([FromQuery] long id, [FromQuery] bool onlyNotHidden = true)
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
        [Route("CreateBid")]
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
        [Route("UpdateBid")]
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
