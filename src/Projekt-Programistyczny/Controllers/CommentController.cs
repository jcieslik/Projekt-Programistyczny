using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using Application.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("GetCommentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentById([FromQuery] long id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpGet]
        [Route("GetCommentsFromOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsFromOffer([FromQuery] long id, [FromQuery] bool onlyNotHidden = true)
        {
            try
            {
                var comments = await _commentService.GetCommentsFromOfferAsync(id, onlyNotHidden);
                return Ok(comments);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetCommentsFromUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsFromUser([FromQuery] long id, [FromQuery] bool onlyNotHidden = true)
        {
            try
            {
                var comments = await _commentService.GetCommentsFromUserAsync(id, onlyNotHidden);
                return Ok(comments);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("UserComments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<CommentDTO>>> GetPaginatedCommentsFromUser(
            [FromBody] SearchCommentsVM vm
            )
        {
            var comments = await _commentService.GetPaginatedCommentsFromUserAsync(vm);
            return Ok(comments);
        }

        [HttpPost]
        [Route("OfferComments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<CommentDTO>>> GetPaginatedCommentsFromOffer(
            [FromBody] SearchCommentsVM vm
            )
        {
            var comments = await _commentService.GetPaginatedCommentsFromOfferAsync(vm);
            return Ok(comments);
        }

        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
        [Route("CreateComment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentDTO>> Create([FromBody] CreateCommentDTO dto)
        {
            try
            {
                var comment = await _commentService.CreateCommentAsync(dto);
                return Ok(comment);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateComment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentDTO>> Update([FromBody] UpdateCommentDTO dto)
        {
            try
            {
                var comment = await _commentService.UpdateCommentAsync(dto);
                return Ok(comment);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
