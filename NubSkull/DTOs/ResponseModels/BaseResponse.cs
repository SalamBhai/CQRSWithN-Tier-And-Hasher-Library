namespace NubSkull.DTOs;

public class BaseResponse<T>
{
    public string Message {get; set;}
    public T Data {get;set;}
    public bool IsSuccessful {get; set;}

}

public class BaseResponse
{
    public string Message {get;set;}
    public bool IsSuccessful {get; set;}
}
