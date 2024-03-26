namespace Application.Common.Exceptions;

public class ExceptionDetails
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }

    public ExceptionDetails(int code, string status, string message)
    {
        Code = code;
        Status = status;
        Message = message;
    }
}