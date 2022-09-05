namespace NubSkull.Contract;

public interface IAuditableEntity
{
    int CreatedBy { get; set; }
    DateTime CreatedOn { get; set; }
    int LastModifiedBy { get; set; }
    DateTime? LastModifiedOn { get; set; }
}
