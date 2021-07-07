using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.DAL.DTO;
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
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService _provinceService;

        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProvinceDTO>> GetProvinceById(long id)
        {
            var province = await _provinceService.GetProvinceByIdAsync(id);
            if(province == null)
            {
                return NotFound();
            }
            return Ok(province);
        }

        [HttpGet]
        [Route("GetProvinces")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProvinceDTO>>> GetProvinces()
        {
            var brands = await _provinceService.GetProvincesAsync();
            return Ok(brands);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [Route("CreateProvince")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ProvinceDTO>> CreateProvince([FromQuery] string name)
        {
            try
            {
                var province = await _provinceService.CreateProvinceAsync(name);
                return Ok(province);
            }
            catch (NameAlreadyInUseException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize(Policy = "CustomerOnly")]
        [Route("UpdateProvince")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ProvinceDTO>> UpdateProvince([FromQuery] UpdateProvinceDTO dto)
        {
            try
            {
                var province = await _provinceService.UpdateProvinceAsync(dto);
                return Ok(province);
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
