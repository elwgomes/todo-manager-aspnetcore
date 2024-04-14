using Core.Entities;

namespace Application.Todos.Http.Response;

public class TodoHttpResponse
{
    public Guid Id { get; }
    public string Title { get; }
    public string? Description { get; }
    public DateTime CreatedAt { get; }
    public DateTime? ConcludedAt { get; }
    public User User { get; set; }
    
    public TodoHttpResponse(Guid id, string title, string description, DateTime createdAt, DateTime? concludedAt, User user)
    {
        Id = id;
        Title = title;
        Description = description;
        CreatedAt = createdAt;
        ConcludedAt = concludedAt;
        User = user;
    }
    
}