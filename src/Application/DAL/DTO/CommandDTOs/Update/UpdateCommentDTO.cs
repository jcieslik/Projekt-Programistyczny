﻿using System;

namespace Application.DAL.DTO.CommandDTOs.Update
{
    public class UpdateCommentDTO
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public bool? IsHidden { get; set; }
    }
}
