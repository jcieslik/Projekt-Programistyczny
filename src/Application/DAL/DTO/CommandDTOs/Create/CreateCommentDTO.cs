using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateCommentDTO
    {
        public long OfferId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; }
    }
}
