namespace $safeprojectname$.Common
{
    // Abstract base class for entities that support auditing
    public abstract class AuditableBaseEntity : BaseEntity
    {
        // The username of the user who created this entity
        public string CreatedBy { get; set; }
        // The timestamp when this entity was created
        public DateTime Created { get; set; }
        // The username of the user who last modified this entity (if applicable)
        public string LastModifiedBy { get; set; }
        // The timestamp when this entity was last modified (if applicable)
        public DateTime? LastModified { get; set; }
    }
}