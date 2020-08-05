namespace WebApi.Entities
{
    public class Register
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string UserLevel { get; set; }
        public string Password { get; set; }
    }
}