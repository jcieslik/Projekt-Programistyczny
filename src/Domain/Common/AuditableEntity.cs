using Domain.Entities;
using System;

namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public User CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public User ModifiedBy { get; set; }
    }
}
