namespace NubSkull.Contract;

public interface ISoftDelete
{
     DateTime? DeletedOn { get; set; }
        int? DeletedBy { get; set; }
        bool IsDeleted { get; set; }
}
