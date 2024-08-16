namespace $safeprojectname$.Entities
{
    public class SalaryRange : AuditableBaseEntity
    {
        // Minimum salary value
        public decimal MinSalary { get; set; }

        // Maximum salary value
        public decimal MaxSalary { get; set; }

        // Name or additional details
        public string Name { get; set; }

        // Navigation property back to Position
        public virtual ICollection<Position> Positions { get; set; }

        public SalaryRange()
        {
            Positions = new HashSet<Position>();
        }

        // Additional properties or methods can be added here
    }
}