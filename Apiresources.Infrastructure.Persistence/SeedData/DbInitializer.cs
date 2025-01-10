namespace $safeprojectname$.SeedData
{
    // Static class for database initialization
    public static class DbInitializer
    {
        // Method to seed data into the database
        public static void SeedData(ApplicationDbContext dbContext)
        {
            // Create an instance of DatabaseSeeder
            var databaseSeeder = new DatabaseSeeder();

            // Insert departments data in bulk
            dbContext.BulkInsert(databaseSeeder.Departments);

            // Insert salary ranges data in bulk
            dbContext.BulkInsert(databaseSeeder.SalaryRanges);

            // Insert positions data in bulk
            dbContext.BulkInsert(databaseSeeder.Positions);

            // Insert employees data in bulk
            dbContext.BulkInsert(databaseSeeder.Employees);
        }
    }
}