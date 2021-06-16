﻿using System;

namespace Application.DAL.DTO.CommandDTOs.Update
{
    public class UpdateUserDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool? IsActive { get; set; }
    }
}
