using System;

namespace Application.DAL.DTO.CommandDTOs.Update
{
    public class UpdateMessageDTO
    {
        public Guid Id { get; set; }
        public bool IsHidden { get; set; }
    }
}
