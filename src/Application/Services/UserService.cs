using Application.Common.Interfaces;
using Application.Common.Services;
using Application.DAL.DTO.CommandDTOs.Create;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : BaseDataService, IUserService
    { 
        public UserService(IApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<User> AuthenticateUser(string login, string password)
        {
            return await _context.Users.Where(u => u.Username == login && u.Password == password).SingleOrDefaultAsync();
        }
            
        public async Task<User> GetUserById(Guid Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<Guid> CreateUserAsync(CreateUserDTO dto)
        {
            var user = new User
            {
                Email = dto.Email,
                Username = dto.Username,
                Password = dto.Password,
                Name = dto.Name,
                Surname = dto.Surname,
                Role = (UserRole)dto.Role,
                IsActive = true
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user.Id;
        }
    }
}
