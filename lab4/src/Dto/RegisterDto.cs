namespace ComfortSpace.Dto
{
    public class RegisterDto
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}