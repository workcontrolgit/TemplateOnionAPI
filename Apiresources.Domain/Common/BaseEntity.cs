namespace $safeprojectname$.Common
{
    /// <summary>
    /// Base class for all entities that have an ID property.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        public virtual Guid Id { get; set; }
    }
}