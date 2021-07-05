using Application.Common.Models;

namespace Application.DAL.Models
{
    public class SearchCommentsVM
    {
        public long SubjectId { get; set; }
        public PaginationProperties Properties { get; set; }
        public bool OnlyNotHidden { get; set; }
    }
}
