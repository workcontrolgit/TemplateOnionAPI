namespace $safeprojectname$.Services
{
    public class DatabaseSeeder
    {
        // Public properties for storing generated data collections
        public IReadOnlyCollection<Department> Departments { get; }
        public IReadOnlyCollection<Employee> Employees { get; }
        public IReadOnlyCollection<Position> Positions { get; }
        public IReadOnlyCollection<SalaryRange> SalaryRanges { get; }

        // Initializing default row counts for the generated data
        public DatabaseSeeder(int rowDepartments = 10,
            int rowSalaryRanges = 5,
            int rowPositions = 100,
            int rowEmployees = 1000,
            int seedValue = 1969)
        {
            // Generate collections based on the specified number of rows and seed value
            Departments = GenerateDepartments(rowDepartments, seedValue);
            SalaryRanges = GenerateSalaryRanges(rowSalaryRanges, seedValue);
            Positions = GeneratePositions(rowPositions, seedValue, Departments, SalaryRanges);
            Employees = GenerateEmployees(rowEmployees, seedValue, Positions);
        }

        private static IReadOnlyCollection<SalaryRange> GenerateSalaryRanges(int rowCount, int seedValue)
        {
            // Setup faker for generating SalaryRange data with specific rules
            var faker = new Faker<SalaryRange>()
                .UseSeed(seedValue)
                .RuleFor(r => r.Id, f => Guid.NewGuid())
                .RuleFor(r => r.Name, f => f.Name.JobDescriptor())
                .RuleFor(r => r.MinSalary, f => f.Random.Number(30000, 40000))
                .RuleFor(r => r.MaxSalary, f => f.Random.Number(80000, 100000))
                .RuleFor(r => r.Created, f => f.Date.Past(f.Random.Number(1, 5), DateTime.Now))
                .RuleFor(r => r.CreatedBy, f => f.Internet.UserName());
            
            // Generate and return the specified number of salary range entries
            return faker.Generate(rowCount);
        }

        private static IReadOnlyCollection<Department> GenerateDepartments(int rowCount, int seedValue)
        {
            // Setup faker for generating Department data with specific rules
            var faker = new Faker<Department>()
                .UseSeed(seedValue)
                .RuleFor(r => r.Id, f => Guid.NewGuid())
                .RuleFor(r => r.Name, f => f.Commerce.Department())
                .RuleFor(r => r.Created, f => f.Date.Past(f.Random.Number(1, 5), DateTime.Now))
                .RuleFor(r => r.CreatedBy, f => f.Internet.UserName());

            // Generate and return the specified number of department entries
            return faker.Generate(rowCount);
        }

        private static IReadOnlyCollection<Employee> GenerateEmployees(int rowCount, int seedValue, IEnumerable<Position> positions)
        {
            // Setup faker for generating Employee data with specific rules, using position information
            var faker = new Faker<Employee>()
                .UseSeed(seedValue)
                .RuleFor(r => r.Id, f => Guid.NewGuid())
                .RuleFor(r => r.Gender, f => f.PickRandom<Gender>())
                .RuleFor(r => r.EmployeeNumber, f => f.Commerce.Ean13())
                .RuleFor(r => r.Salary, f => f.Random.Number(20000, 110000))
                .RuleFor(r => r.Prefix, (f, r) => f.Name.Prefix((Name.Gender)r.Gender))
                .RuleFor(r => r.FirstName, (f, r) => f.Name.FirstName((Name.Gender)r.Gender))
                .RuleFor(r => r.MiddleName, (f, r) => f.Name.FirstName((Name.Gender)r.Gender))
                .RuleFor(r => r.LastName, (f, r) => f.Name.LastName((Name.Gender)r.Gender))
                .RuleFor(r => r.Birthday, f => f.Person.DateOfBirth)
                .RuleFor(r => r.Email, (f, p) => f.Internet.Email(p.FirstName, p.LastName))
                .RuleFor(r => r.Phone, f => f.Phone.PhoneNumber("(###)-###-####"))
                .RuleFor(r => r.PositionId, f => f.PickRandom(positions).Id)
                .RuleFor(r => r.Created, f => f.Date.Past(f.Random.Number(1, 5), DateTime.Now))
                .RuleFor(r => r.CreatedBy, f => f.Internet.UserName());

            // Generate and return the specified number of employee entries
            return faker.Generate(rowCount);
        }

        private static IReadOnlyCollection<Position> GeneratePositions(
            int rowCount, int seedValue,
            IEnumerable<Department> departments,
            IEnumerable<SalaryRange> salaryRanges)
        {
            // Setup faker for generating Position data with specific rules, using department and salary range information
            var random = new Random();
            var faker = new Faker<Position>()
                .UseSeed(seedValue)
                .RuleFor(r => r.Id, f => Guid.NewGuid())
                .RuleFor(o => o.PositionTitle, f => f.Name.JobTitle())
                .RuleFor(o => o.PositionNumber, f => f.Commerce.Ean13())
                .RuleFor(o => o.PositionDescription, f => f.Lorem.Paragraphs(2))
                .RuleFor(r => r.DepartmentId, f => f.PickRandom(departments).Id)
                .RuleFor(r => r.SalaryRangeId, f => f.PickRandom(salaryRanges).Id)
                .RuleFor(r => r.Created, f => f.Date.Past(f.Random.Number(1, 5), DateTime.Now))
                .RuleFor(r => r.CreatedBy, f => f.Internet.UserName());

            // Generate, deduplicate by department and salary range, and return the specified number of position entries
            return faker.Generate(rowCount)
                .GroupBy(r => new { r.DepartmentId, r.SalaryRangeId })
                .Select(r => r.First())
                .ToList();
        }
    }
}