namespace $safeprojectname$.Features.Positions.Queries.GetPositions
{
    public class GetPositionsViewModel
    {
        public Guid Id { get; set; }
        public string PositionTitle { get; set; }
        public string PositionNumber { get; set; }
        public string PositionDescription { get; set; }
        public virtual Department Department { get; set; }
        public virtual SalaryRange SalaryRange { get; set; }
    // ViewModel class for representing positions.
    public class GetPositionsViewModel
    {
        // Unique identifier for each position.
        public Guid Id { get; set; }

        // Title of the position (e.g., Senior Developer).
        public string PositionTitle { get; set; }

        // Identifier number associated with a specific position.
        public string PositionNumber { get; set; }

        // Detailed description of the responsibilities and requirements for this position.
        public string PositionDescription { get; set; }

        // Associated department that holds the position.
        public virtual Department Department { get; set; }

        // Salary range assigned to the position.
        public virtual SalaryRange SalaryRange { get; set; }
    }
}