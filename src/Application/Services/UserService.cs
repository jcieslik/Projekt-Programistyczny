using Application.Common.Interfaces;
using Application.Common.Services;
using AutoMapper;
using Domain.Entities;
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
    }
}
