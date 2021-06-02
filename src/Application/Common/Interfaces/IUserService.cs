using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateUser(string login, string password);
        Task<User> GetUserById(Guid Id);
    }
}
