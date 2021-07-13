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
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet("GetProductCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetProductCategoryById(long id)
        {
            var category = await _productCategoryService.GetProductCategoryByIdAsync(id);
            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet]
        [Route("GetProductCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetProductCategories()
        {
            var categories = await _productCategoryService.GetProductCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("GetProductCategoriesByIds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetProductCategoriesByIds(List<long> ids)
        {
            var categories = await _productCategoryService.GetProductCategoriesByIdsAsync(ids);
            return Ok(categories);
        }


        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [Route("CreateProductCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ProductCategoryDTO>> CreateProductCategory([FromBody] CreateProductCategoryDTO dto)
        {
            try
            {
                var category = await _productCategoryService.CreateProductCategoryAsync(dto);
                return Ok(category);
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

        [HttpPut]
        [Authorize(Policy = "AdminOnly")]
        [Route("UpdateProductCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ProductCategoryDTO>> UpdateProductCategory([FromBody] UpdateProductCategoryDTO dto)
        {
            try
            {
                var category = await _productCategoryService.UpdateProductCategoryAsync(dto);
                return Ok(category);
            }
            catch (NameAlreadyInUseException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
