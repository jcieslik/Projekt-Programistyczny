using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : Controller
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        public RateController(IRateService rateService, IMapper mapper)
        {
            _rateService = rateService;
        }

        [HttpGet]
        [Route("GetRateById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductRateDTO>> GetRateById([FromQuery] Guid id)
        {
            var rate = await _rateService.GetRateByIdAsync(id);
            if(rate == null)
            {
                return NotFound();
            }
            return Ok(rate);
        }

        [HttpGet]
        [Route("GetRatesFromOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductRateDTO>>> GetRatesFromOffer([FromQuery] Guid id, [FromQuery] bool onlyNotHidden = true)
        {
            try
            {
                var rates = await _rateService.GetRatesFromOfferAsync(id, onlyNotHidden);
                return Ok(rates);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetRatesFromUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductRateDTO>>> GetRatesFromUser([FromQuery] Guid id, [FromQuery] bool onlyNotHidden = true)
        {
            try
            {
                var rates = await _rateService.GetRatesFromUserAsync(id, onlyNotHidden);
                return Ok(rates);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductRateDTO>> Create([FromBody] CreateProductRateDTO dto)
        {
            try
            {
                var rate = await _rateService.CreateRateAsync(dto);
                return Ok(rate);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductRateDTO>> Update([FromBody] UpdateProductRateDTO dto)
        {
            try
            {
                var rate = await _rateService.UpdateRateAsync(dto);
                return Ok(rate);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
