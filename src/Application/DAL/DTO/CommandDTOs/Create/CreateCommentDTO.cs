namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateCommentDTO
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public string Content { get; set; }
        public double RateValue { get; set; }
    }
}
