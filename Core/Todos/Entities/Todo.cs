namespace Core.Entities;

public class Todo
{
    public Guid Id { get; private set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ConcludedAt { get; set; }
    public Guid UserId { get; set; }
}