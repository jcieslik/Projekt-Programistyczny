using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IMessageService
    {
        Task<BaseMessageDTO> GetMessageByIdAsync(long id);
        Task<BaseMessageDTO> CreateMessageAsync(CreateMessageDTO dto);
        Task<MessageDTO> CreateTransmissionAsync(CreateTransmissionDTO dto);
        Task<MessageDTO> GetMessageTransmissionByIdAsync(long id);
        Task<PaginatedList<MessageDTO>> GetPaginatedMessagesFromUserAsync(long userId, int mailboxType, PaginationProperties properties);
        Task<BaseMessageDTO> UpdateMessageAsync(UpdateMessageDTO dto);
        Task<MessageDTO> UpdateTransmissionAsync(UpdateTransmissionDTO dto);
    }
}