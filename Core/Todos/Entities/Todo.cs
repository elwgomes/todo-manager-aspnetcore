namespace Core.Entities;

public class Todo
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ConcludedAt { get; set; }
    public Guid UserId { get; set; }

    public User User { get; set; }
    
    public Todo()
    {
        
    }

    public Todo(string title, string? description)
    {
        Title = title;
        Description = description;
    }

    public Todo(string title, string? description, DateTime createdAt, DateTime? concludedAt)
    {
        Title = title;
        Description = description;
        CreatedAt = createdAt;
        ConcludedAt = concludedAt;
    }

    public Todo(string title, string? description, DateTime createdAt, Guid userId, User user)
    {
        Title = title;
        Description = description;
        CreatedAt = createdAt;
        UserId = userId;
        User = user;
    }
}