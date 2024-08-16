namespace $safeprojectname$.Mock
{
    public class PositionInsertBogusConfig : Faker<Position>
    {
        public PositionInsertBogusConfig(IEnumerable<Department> departments, IEnumerable<SalaryRange> salaryRanges)
        {
            RuleFor(o => o.Id, f => Guid.NewGuid());
            RuleFor(o => o.PositionTitle, f => f.Name.JobTitle());
            RuleFor(o => o.PositionNumber, f => f.Commerce.Ean13());
            RuleFor(o => o.PositionDescription, f => f.Name.JobDescriptor());
            RuleFor(o => o.DepartmentId, f => f.PickRandom(departments).Id);
            RuleFor(o => o.SalaryRangeId, f => f.PickRandom(salaryRanges).Id);
            RuleFor(o => o.Created, f => f.Date.Past(1));
            RuleFor(o => o.CreatedBy, f => f.Name.FullName());
            RuleFor(o => o.LastModified, f => f.Date.Recent(1));
            RuleFor(o => o.LastModifiedBy, f => f.Name.FullName());
        }
    }
}
