namespace Core.Users.Exceptions;

public class CustomException : Exception
{
    public CustomException() : base("Something goes wrong...")
    {
    }
}