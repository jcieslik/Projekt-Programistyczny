using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [Route("GetBrands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrands()
        {
            var brands = await _brandService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpPost]
        [Route("CreateBrand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BrandDTO>> Create([FromQuery] string name)
        {
            try
            {
                var brand = await _brandService.CreateBrandAsync(name);
                return Ok(brand);
            }
            catch(NameAlreadyInUseException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateBrand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BrandDTO>> Update([FromBody] UpdateBrandDTO dto)
        {
            try
            {
                var brand = await _brandService.UpdateBrandAsync(dto);
                return Ok(brand);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (NameAlreadyInUseException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
