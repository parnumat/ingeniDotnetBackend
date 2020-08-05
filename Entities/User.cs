using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string UserLevel { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}