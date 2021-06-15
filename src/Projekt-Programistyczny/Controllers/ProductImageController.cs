using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
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
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpGet]
        [Route("GetImageById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductImageDTO>> GetImageById([FromQuery] Guid id)
        {
            var image = await _productImageService.GetProductImageByIdAsync(id);
            if(image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }

        [HttpGet]
        [Route("GetImagesFromOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductImageDTO>>> GetImagesFromOffer([FromQuery] Guid id, [FromQuery] bool onlyNotHidden = true)
        {
            try
            {
                var images = await _productImageService.GetProductImagesFromOfferdAsync(id, onlyNotHidden);
                return Ok(images);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("CreateImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductImageDTO>> Create([FromBody] CreateProductImageDTO dto)
        {
            try
            {
                var image = await _productImageService.CreateProductImageAsync(dto);
                return Ok(image);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductImageDTO>> Update([FromBody] UpdateProductImageDTO dto)
        {
            try
            {
                var image = await _productImageService.UpdateProductImageAsync(dto);
                return Ok(image);
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
