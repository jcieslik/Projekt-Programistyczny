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
        Task<long> CreateMessageAsync(CreateMessageDTO dto);
        Task<MessageDTO> GetMessageByIdAsync(long id);
        Task<PaginatedList<MessageDTO>> GetPaginatedMessagesFromUserAsync(long userId, PaginationProperties properties);
        Task<long> UpdateMessageAsync(UpdateMessageDTO dto);
    }
}