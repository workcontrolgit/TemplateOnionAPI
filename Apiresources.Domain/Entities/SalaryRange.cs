namespace $safeprojectname$.Entities
{
    // Represents a salary range for a position
    public class SalaryRange : AuditableBaseEntity
    {
        // Minimum salary value within this range
        public decimal MinSalary { get; set; }

        // Maximum salary value within this range
        public decimal MaxSalary { get; set; }

        // Name or additional details about the salary range
        public string Name { get; set; }

        // Navigation property back to Position, indicating which positions fall within this salary range
        public virtual ICollection<Position> Positions { get; set; }

        // Initializes a new instance of the SalaryRange class with an empty collection of Positions
        public SalaryRange()
        {
            Positions = new HashSet<Position>();
        }

        // Additional properties or methods can be added here to further define the behavior and functionality of this entity
    }
}