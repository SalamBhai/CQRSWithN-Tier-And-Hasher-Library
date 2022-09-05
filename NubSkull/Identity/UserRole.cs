using NubSkull.Contract;

namespace NubSkull.Identity;

public class UserRole : AuditableEntity
{
    public int RoleId {get;set;}
    public Role Role {get; set;}
    public int UserId {get;set;}
    public User User{get; set;}
}
