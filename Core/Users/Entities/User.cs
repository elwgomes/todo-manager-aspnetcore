using System.Text.Json.Serialization;

namespace Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLogin { get; set; }

    [JsonIgnore]
    public ICollection<Todo> Todos { get; set; }
    
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public User(string username, string password, DateTime createdAt)
    {
        Username = username;
        Password = password;
        CreatedAt = createdAt;
    }
    
}