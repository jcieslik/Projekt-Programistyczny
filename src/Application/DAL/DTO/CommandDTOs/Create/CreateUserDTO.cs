namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateUserDTO
    {
        public int Role { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long ProvinceId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
    }
}
