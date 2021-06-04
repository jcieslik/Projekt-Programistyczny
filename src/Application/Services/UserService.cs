using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext context;

        public UserService(IApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        public async Task<User> AuthenticateUser(string login, string password)
        {
            return await context.Users.Where(u => u.Username == login && u.Password == password).SingleOrDefaultAsync();
        }
            
        public async Task<User> GetUserById(Guid Id)
        {
            return await context.Users.FindAsync(Id);
        }
    }
}
