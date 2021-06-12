using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
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

        public async Task<UserDTO> AuthenticateUser(string login, string password)
        {
            return _mapper.Map<UserDTO>(await _context.Users.Where(u => u.Username == login && u.Password == password).SingleOrDefaultAsync());
        }
            
        public async Task<UserDTO> GetUserById(Guid Id)
        {
            return _mapper.Map<UserDTO>(await _context.Users.FindAsync(Id));
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO dto)
        {
            var emailCheck = await _context.Users.Where(x => x.Email == dto.Email).CountAsync();
            if(emailCheck > 0)
            {
                throw new EmailAlreadyInUseException(dto.Email);
            }

            var usernameCheck = await _context.Users.Where(x => x.Username == dto.Username).CountAsync();
            if (usernameCheck > 0)
            {
                throw new NameAlreadyInUseException(dto.Username);
            }

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

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUserAsync(UpdateUserDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.Id);
            if(user == null)
            {
                throw new NotFoundException(nameof(User), dto.Id);
            }

            if (!string.IsNullOrEmpty(dto.Email)){
                var emailCheck = await _context.Users.Where(x => x.Email == dto.Email).CountAsync();
                if (emailCheck > 0)
                {
                    throw new EmailAlreadyInUseException(dto.Email);
                }
                user.Email = dto.Email;
            }

            if (!string.IsNullOrEmpty(dto.Username)){
                var usernameCheck = await _context.Users.Where(x => x.Username == dto.Username).CountAsync();
                if (usernameCheck > 0)
                {
                    throw new NameAlreadyInUseException(dto.Username);
                }
                user.Username = dto.Username;
            }

            if (!string.IsNullOrEmpty(dto.Password)){
                user.Password = dto.Password;
            }

            if (!string.IsNullOrEmpty(dto.Name)){
                user.Name = dto.Name;
            }

            if (!string.IsNullOrEmpty(dto.Surname)){
                user.Surname = dto.Surname;
            }

            if (dto.IsAsctive.HasValue)
            {
                user.IsActive = dto.IsAsctive.Value;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);
        }
    }
}
