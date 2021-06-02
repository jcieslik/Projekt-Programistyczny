using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    public class UserDTO
    {
        public string Username { get; set; }
        public UserRole Role { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public UserDTO(User user)
        {
            Username = user.Username;
            Role = user.Role;
            Email = user.Email;
        }
    }
}
