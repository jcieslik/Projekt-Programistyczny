using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class CommentDTO : EntityDTO, IMapFrom<Comment>
    {
        public string Content { get; set; }
        public OfferDTO Offer { get; set; }
        public UserDTO Customer { get; set; }
    }
}
