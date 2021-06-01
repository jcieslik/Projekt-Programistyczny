using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Comment : AuditableEntity
    {
        public string Content { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid CustomerId { get; set; }
        public User Customer { get; set; }
    }
}
