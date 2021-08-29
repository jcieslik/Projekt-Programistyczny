using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt_Programistyczny.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        [Authorize]
        [Route("GetMessageById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseMessageDTO>> GetMessageById([FromQuery] long id)
        {
            var message = await _messageService.GetMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpGet]
        [Authorize]
        [Route("GetTransmissionById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MessageDTO>> GetMessageTransmissionById([FromQuery] long id)
        {
            var transmission = await _messageService.GetMessageTransmissionByIdAsync(id);
            if (transmission == null)
            {
                return NotFound();
            }
            return Ok(transmission);
        }

        [HttpPost]
        [Authorize]
        [Route("GetMessagesFromUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaginatedList<MessageDTO>>> GetPaginatedMessagesFromUser([FromBody] PaginationProperties properties, [FromQuery] MailboxType mailboxType, [FromQuery] string searchText)
        {
            try
            {
                var messages = await _messageService.GetPaginatedMessagesFromUserAsync(HttpContext.User.GetUserId(), mailboxType, properties, searchText);
                return Ok(messages);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("CreateMessage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseMessageDTO>> CreateMessage([FromBody] CreateMessageDTO dto)
        {
            try
            {
                var message = await _messageService.CreateMessageAsync(dto);
                return Ok(message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateMessage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseMessageDTO>> UpdateMessage([FromBody] UpdateMessageDTO dto)
        {
            try
            {
                var message = await _messageService.UpdateMessageAsync(dto);
                return Ok(message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetNumberOfUnreadMessages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MessageDTO>> GetNumberOfUnreadMessages()
        {
            try
            {
                var message = await _messageService.GetNumberOfUnreadMessages(HttpContext.User.GetUserId());
                return Ok(message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        [Route("ChangeMessagesStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ChangeMessagesStatus([FromBody] List<long> messageIds, [FromQuery] bool IsRead)
        {
            if(await _messageService.ChangeMessagesStatus(messageIds, HttpContext.User.GetUserId(), IsRead))
            {
                return Ok(true);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteMessages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteMessages([FromBody] List<long> messageIds)
        {
            if (await _messageService.DeleteMessages(messageIds, HttpContext.User.GetUserId()))
            {
                return Ok(true);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("TakeMessagesFromTrash")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> TakeMessagesFromTrash([FromBody] List<long> messageIds)
        {
            if (await _messageService.TakeMessagesFromTrash(messageIds, HttpContext.User.GetUserId()))
            {
                return Ok(true);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
