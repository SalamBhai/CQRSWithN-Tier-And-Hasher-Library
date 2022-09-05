namespace NubSkull.DTOs;

public class RoleDto
{
    public int RoleId{get;set;}
    public string Name {get; set; }
    public string Description {get; set;}
}

public class CreateRoleRequestModel
{
    public string Name {get; set; }
    public string Description {get; set;}
}

public class UpdateRoleRequestModel
{
    public string Name {get; set; }
    public string Description {get; set;}
}