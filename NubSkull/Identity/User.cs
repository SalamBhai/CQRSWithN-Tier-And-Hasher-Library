using NubSkull.Contract;

namespace NubSkull.Identity;

public class User : AuditableEntity
{
    public string UserName {get;set;}
    public string HashedPassword {get; set;}
    public  string NormalizedPassword { private get; set;}
    public string EmailAddress{get; set;}
    public ICollection<UserRole> UserRoles = new HashSet<UserRole>();
    
}
