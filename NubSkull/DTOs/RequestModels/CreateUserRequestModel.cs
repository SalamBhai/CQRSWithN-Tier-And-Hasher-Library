namespace NubSkull.DTOs.RequestModels;

public class CreateUserRequestModel
{
    public string UserName {get; set;}
    public string Password {get; set;}
    public string EmailAddress {get; set;}
    public List<string> RoleNames{get; set;}
    
}
