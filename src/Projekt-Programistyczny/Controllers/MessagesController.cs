using Application.Common.Exceptions;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Route("GetMessageFromUser/user/{userId}/type/{mailboxType}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaginatedList<MessageDTO>>> GetPaginatedMessagesFromUser(
            [FromBody] PaginationProperties properties,
            [FromRoute] long userId,
            [FromRoute] int mailboxType
            )
        {
            try
            {
                var messages = await _messageService.GetPaginatedMessagesFromUserAsync(userId, mailboxType, properties);
                return Ok(messages);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
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

        [HttpPost]
        [Route("CreateTransmission")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MessageDTO>> CreateTransmission([FromBody] CreateTransmissionDTO dto)
        {
            try
            {
                var transmission = await _messageService.CreateTransmissionAsync(dto);
                return Ok(transmission);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
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

        [HttpPut]
        [Route("UpdateTransmission")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MessageDTO>> UpdateTransmission([FromBody] UpdateTransmissionDTO dto)
        {
            try
            {
                var message = await _messageService.UpdateTransmissionAsync(dto);
                return Ok(message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
