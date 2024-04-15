using System.Text.Json.Serialization;

namespace Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
    
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public DateTime? LastLogin { get; set; }

    [JsonIgnore]
    public ICollection<Todo> Todos { get; set; }

    public User()
    {
        
    }
    
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public User(Guid id, string username)
    {
        Id = id;
        Username = username;
    }
    
    public User(string username, string password, DateTime createdAt)
    {
        Username = username;
        Password = password;
        CreatedAt = createdAt;
    }
    
}