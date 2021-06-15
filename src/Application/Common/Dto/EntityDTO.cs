using System;

namespace Application.Common.Dto
{
    public abstract class EntityDTO
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public long ModifiedBy { get; set; }
    }
}
