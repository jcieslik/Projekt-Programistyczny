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
    }
}
