using NubSkull.Contract;

namespace NubSkull.Identity;

public class Role : AuditableEntity
{
    public string Name{get;set;}
    public string Description {get; set;}
    public ICollection<UserRole> UserRoles = new HashSet<UserRole>();
}
