using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        public long Id { get; set; }

        [Required]
        public DateTime Created { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public long ModifiedBy { get; set; }
    }
}
