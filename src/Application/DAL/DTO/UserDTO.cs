using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.DAL.DTO
{
    public class UserDTO : EntityDTO, IMapFrom<User>
    {
        public string Username { get; set; }
        public UserRole Role { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
