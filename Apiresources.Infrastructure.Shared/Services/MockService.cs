namespace $safeprojectname$.Services
{
    // MockService class implementing IMockService interface
    public class MockService : IMockService
    {

        // Method to get a list of generated Position objects
        // rowCount: Number of Position objects to generate
        // departments: Collection of Department objects to associate with Positions
        // salaryRanges: Collection of SalaryRange objects to associate with Positions
        public List<Position> GetPositions(int rowCount, IEnumerable<Department> departments, IEnumerable<SalaryRange> salaryRanges)
        {
            // Create a new faker instance with specified departments and salary ranges
            var faker = new PositionInsertBogusConfig(departments, salaryRanges);
            // Generate and return a list of Position objects
            return faker.Generate(rowCount);
        }

        // Method to get a list of generated Employee objects
        // rowCount: Number of Employee objects to generate
        public List<Employee> GetEmployees(int rowCount)
        {
            // Create a new faker instance for Employee objects
            var faker = new EmployeeBogusConfig();
            // Generate and return a list of Employee objects
            return faker.Generate(rowCount);
        }

        // Method to seed a list of Position objects
        // rowCount: Number of Position objects to generate
        public List<Position> SeedPositions(int rowCount)
        {
            // Create a new faker instance for seeding Position objects
            var faker = new PositionSeedBogusConfig();
            // Generate and return a list of seeded Position objects
            return faker.Generate(rowCount);
        }
    }
}