namespace $safeprojectname$.Entities
{
    public class Position : AuditableBaseEntity
    {
        // Property representing the title of the position.
        public string PositionTitle { get; set; }

        // Property representing the number of the position.
        public string PositionNumber { get; set; }

        // Property representing a description of the position.
        public string PositionDescription { get; set; }

        // Foreign key property for the associated Department entity.
        public Guid DepartmentId { get; set; }

        // Navigation property for the associated Department entity.
        public virtual Department Department { get; set; }

        // Navigation property for related Employee entities.
        public virtual ICollection<Employee> Employees { get; set; }

        // Foreign key property for the associated SalaryRange entity.
        public Guid SalaryRangeId { get; set; }

        // Navigation property for the associated SalaryRange entity.
        public virtual SalaryRange SalaryRange { get; set; }

        // Default constructor that initializes the Employees collection.
        public Position()
        {
            Employees = new HashSet<Employee>();
        }
    }
}