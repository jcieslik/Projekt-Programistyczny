using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
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
        public long CartId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDTO>()
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Cart.Id));
        }
    }
}
