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
    }
}