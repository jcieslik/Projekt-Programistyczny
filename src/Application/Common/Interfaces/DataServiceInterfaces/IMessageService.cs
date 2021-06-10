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
        Task<Guid> CreateMessageAsync(CreateMessageDTO dto);
        Task<MessageDTO> GetMessageByIdAsync(Guid id);
        Task<PaginatedList<MessageDTO>> GetPaginatedMessagesFromUserAsync(Guid userId, PaginationProperties properties);
        Task<Guid> UpdateMessageAsync(UpdateMessageDTO dto);
    }
}