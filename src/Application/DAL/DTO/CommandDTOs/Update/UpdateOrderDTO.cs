﻿using System;

namespace Application.DAL.DTO.CommandDTOs.Update
{
    public class UpdateOrderDTO
    {
        public Guid Id { get; set; }
        public int OrderStatus { get; set; }
    }
}