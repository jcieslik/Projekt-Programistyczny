﻿using Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class City : AuditableEntity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Offer> Offers { get; set; }
    }
}
