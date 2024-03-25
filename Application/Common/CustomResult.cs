namespace Application.Common;

public class CustomResult<D>
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public D? Data { get; set; }

    public CustomResult(int code, string status, string message, D? data = default)
    {
        Code = code;
        Status = status;
        Message = message;
        Data = data;
    }
}