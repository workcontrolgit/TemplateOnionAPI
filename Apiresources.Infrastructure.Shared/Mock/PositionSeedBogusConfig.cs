namespace $safeprojectname$.Mock
{
    // Class `PositionSeedBogusConfig` inherits from `AutoFaker<Position>`
    public class PositionSeedBogusConfig : AutoFaker<Position>
    {
        // Constructor to define the rules and seed for generating fake `Position` data
        public PositionSeedBogusConfig()
        {
            // Set the seed for random data generation for consistency in data
            Randomizer.Seed = new Random(8675309);
            
            // Rule to assign a unique identifier to `Id` property
            RuleFor(m => m.Id, f => Guid.NewGuid());
            
            // Rule to generate a job title and assign to `PositionTitle` property
            RuleFor(o => o.PositionTitle, f => f.Name.JobTitle());
            
            // Rule to generate a 13-digit EAN code and assign to `PositionNumber` property
            RuleFor(o => o.PositionNumber, f => f.Commerce.Ean13());
            
            // Rule to generate a job description and assign to `PositionDescription` property
            RuleFor(o => o.PositionDescription, f => f.Name.JobDescriptor());
            
            // Rule to generate a past date within the last year for the `Created` property
            RuleFor(o => o.Created, f => f.Date.Past(1));
            
            // Rule to generate a full name and assign to the `CreatedBy` property
            RuleFor(o => o.CreatedBy, f => f.Name.FullName());
            
            // Rule to generate a recent date within the last day for the `LastModified` property
            RuleFor(o => o.LastModified, f => f.Date.Recent(1));
            
            // Rule to generate a full name and assign to the `LastModifiedBy` property
            RuleFor(o => o.LastModifiedBy, f => f.Name.FullName());
        }
    }
}