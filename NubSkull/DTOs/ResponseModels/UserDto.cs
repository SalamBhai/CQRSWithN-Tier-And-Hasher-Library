namespace NubSkull.DTOs;

public class UserDto
{
    public int Id {get;set;}
    public string UserName {get;set;}
    public string EmailAddress{get;set;}
    public List<string> UserRoles {get; set;}
}

