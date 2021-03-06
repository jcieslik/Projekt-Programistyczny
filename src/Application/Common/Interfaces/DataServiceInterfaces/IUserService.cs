using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IUserService
    {
        Task<UserDTO> AuthenticateUser(string login, string password);
        Task<UserDTO> GetUserById(long Id);
        Task<IEnumerable<RecipientDTO>> GetAllUsers();
        Task<UserDTO> CreateUserAsync(CreateUserDTO dto);
        Task<UserDTO> UpdateUserAsync(UpdateUserDTO dto);
        Task<PaginatedList<UserDTO>> GetPaginatedUsers(PaginationProperties properties, bool onlyActive = false);
        Task BanUser(string banInfo, long userId); 
        Task UnbanUser(long userId); 
    }
}
