using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Common.Services;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return _mapper.Map<UserDTO>(
                await _context.Users.Include(x => x.Cart)
                .Where(u => u.Username == login && u.Password == password)
                .SingleOrDefaultAsync());
        }
            
        public async Task<UserDTO> GetUserById(long Id)
        {
            return _mapper.Map<UserDTO>(await _context.Users.Include(u => u.Province).Where(u => u.Id == Id).FirstOrDefaultAsync());
        }


        public async Task<IEnumerable<RecipientDTO>> GetAllUsers()
        {
            var users = _context.Users
                .AsNoTracking();

            return await users.ProjectTo<RecipientDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PaginatedList<UserDTO>> GetPaginatedUsers(PaginationProperties properties, bool onlyActive = false)
        {
            var users = _context.Users.Include(u => u.Cart).Include(u => u.Province).AsNoTracking();

            users = onlyActive ? users.Where(x => x.IsActive) : users;

            users = users.Where(u => u.Role == UserRole.Customer);

            users = properties.OrderBy switch
            {
                "id" => users.OrderBy(x => x.Id),
                "username" => users.OrderBy(x => x.Username),
                "name" => users.OrderBy(x => x.Surname).ThenBy(x => x.Name),
                _ => users.OrderBy(x => x.Id)
            };

            return await users.ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync<UserDTO>(properties.PageIndex, properties.PageSize);
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

            var province = await _context.Provinces.FindAsync(dto.ProvinceId);
            if (province == null)
            {
                throw new NotFoundException(nameof(Province), dto.ProvinceId);
            }

            var user = new User
            {
                Email = dto.Email,
                Username = dto.Username,
                Password = dto.Password,
                Name = dto.Name,
                Surname = dto.Surname,
                Role = (UserRole)dto.Role,
                Province = province,
                City = dto.City,
                Street = dto.Street,
                PostCode = dto.PostCode,
                IsActive = true
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            var userCart = new Cart
            {
                CustomerId = user.Id
            };

            _context.Carts.Add(userCart);
            await _context.SaveChangesAsync();

            user.Cart = userCart;
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUserAsync(UpdateUserDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.Id);
            if(user == null)
            {
                throw new NotFoundException(nameof(User), dto.Id);
            }

            if (dto.ProvinceId.HasValue)
            {
                var province = await _context.Provinces.FindAsync(dto.ProvinceId);
                user.Province = province ?? throw new NotFoundException(nameof(Province), dto.ProvinceId);
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

            if (!string.IsNullOrEmpty(dto.City))
            {
                user.City = dto.City;
            }

            if (!string.IsNullOrEmpty(dto.Street))
            {
                user.Street = dto.Street;
            }

            if (!string.IsNullOrEmpty(dto.PostCode))
            {
                user.PostCode = dto.PostCode;
            }

            if (!string.IsNullOrEmpty(dto.BankAccountNumber))
            {
                user.BankAccountNumber = dto.BankAccountNumber;
            }

            if (dto.IsActive.HasValue)
            {
                user.IsActive = dto.IsActive.Value;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);
        }

        public async Task BanUser(string banInfo, long userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                user.IsActive = false;
                user.BanInfo = banInfo;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException(nameof(User), userId);
            }
        }

        public async Task UnbanUser(long userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                user.IsActive = true;
                user.BanInfo = "";
                await _context.SaveChangesAsync();
            }
            else { 
                throw new NotFoundException(nameof(User), userId);
            }
        }
    }
}
