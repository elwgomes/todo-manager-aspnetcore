namespace Application.Users.Http.Response;

public class UserHttpResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    
    public UserHttpResponse()
    {
        
    }
    
    public UserHttpResponse(Guid id, string username)
    {
        Id = id;
        Username = username;
    }
}