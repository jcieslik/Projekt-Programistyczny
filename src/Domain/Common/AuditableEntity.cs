using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
