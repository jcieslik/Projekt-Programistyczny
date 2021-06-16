using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CityDTO>> GetCityById(long id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            if(city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpGet]
        [Route("GetCities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCities()
        {
            var cities = await _cityService.GetCitiesAsync();
            return Ok(cities);
        }

        [HttpPost]
        [Route("CreateCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BrandDTO>> CreateCity([FromQuery] string name)
        {
            try
            {
                var brand = await _cityService.CreateCityAsync(name);
                return Ok(brand);
            }
            catch (NameAlreadyInUseException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BrandDTO>> UpdateCity([FromQuery] UpdateCityDTO dto)
        {
            try
            {
                var brand = await _cityService.UpdateCityAsync(dto);
                return Ok(brand);
            }
            catch (NameAlreadyInUseException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}