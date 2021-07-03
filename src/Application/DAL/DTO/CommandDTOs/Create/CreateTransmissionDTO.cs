namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateTransmissionDTO
    {
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public long MailboxOwnerId { get; set; }
        public long MessageId { get; set; }
        public int MailboxType { get; set; }
    }
}
