namespace $safeprojectname$.Entities
{
    public class Position : AuditableBaseEntity
    {
        public string PositionTitle { get; set; }
        public string PositionNumber { get; set; }
        public string PositionDescription { get; set; }

        // Foreign Key for Department
        public Guid DepartmentId { get; set; }

        // Navigation Property for Department
        public virtual Department Department { get; set; }

        // Navigation Property for related Employees
        public virtual ICollection<Employee> Employees { get; set; }

        // Foreign Key for SalaryRange
        public Guid SalaryRangeId { get; set; }

        // Navigation Property for SalaryRange
        public virtual SalaryRange SalaryRange { get; set; }

        public Position()
        {
            Employees = new HashSet<Employee>();
        }

    }
}