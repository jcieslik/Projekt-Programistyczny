using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public abstract class AuditableAndAbleToBeHiddenEntity : AuditableEntity
    {
        [Required]
        public bool IsHidden { get; set; }
    }
}
