using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateCommentDTO
    {
        public Guid OfferId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}
