namespace $safeprojectname$.Mock
{
    // This class configures bogus data generation for the Position entity using Faker.
    public class PositionInsertBogusConfig : Faker<Position>
    {
        // Constructor that takes in collections of departments and salary ranges 
        // to aid in generating realistic test data.
        public PositionInsertBogusConfig(IEnumerable<Department> departments, IEnumerable<SalaryRange> salaryRanges)
        {
            // Assign a new Guid for the Id property for each generated Position.
            RuleFor(o => o.Id, f => Guid.NewGuid());

            // Generate a random job title for the PositionTitle property.
            RuleFor(o => o.PositionTitle, f => f.Name.JobTitle());

            // Generate a random 13-digit EAN number for the PositionNumber property.
            RuleFor(o => o.PositionNumber, f => f.Commerce.Ean13());

            // Create a random description associated with a job for PositionDescription.
            RuleFor(o => o.PositionDescription, f => f.Name.JobDescriptor());

            // Pick a random Department's Id from the list provided for DepartmentId.
            RuleFor(o => o.DepartmentId, f => f.PickRandom(departments).Id);

            // Pick a random SalaryRange's Id from the list provided for SalaryRangeId.
            RuleFor(o => o.SalaryRangeId, f => f.PickRandom(salaryRanges).Id);

            // Generate a random past date within the last year for the Created property.
            RuleFor(o => o.Created, f => f.Date.Past(1));

            // Generate a random full name for the CreatedBy property.
            RuleFor(o => o.CreatedBy, f => f.Name.FullName());

            // Generate a random recent date within the last day for the LastModified property.
            RuleFor(o => o.LastModified, f => f.Date.Recent(1));

            // Generate a random full name for the LastModifiedBy property.
            RuleFor(o => o.LastModifiedBy, f => f.Name.FullName());
        }
    }
}
