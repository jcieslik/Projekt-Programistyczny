namespace Application.DAL.DTO.CommandDTOs.Update
{
    public class UpdateTransmissionDTO
    {
        public long Id { get; set; }
        public int? MailboxType { get; set; }
        public bool? IsHidden { get; set; }
    }
}
