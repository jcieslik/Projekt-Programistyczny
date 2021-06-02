namespace Domain.Common
{
    public abstract class AuditableAndAbleToBeHiddenEntity : AuditableEntity
    {
        public bool IsHidden { get; set; }
    }
}
