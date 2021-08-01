using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class RecipientDTO : EntityDTO, IMapFrom<User>
    {
        public string Username { get; set; }
    }
}
