namespace $safeprojectname$.SeedData
{
    public static class DbInitializer
    {
        public static void SeedData(ApplicationDbContext dbContext)
        {
            var databaseSeeder = new DatabaseSeeder();
            dbContext.BulkInsert(databaseSeeder.Departments);
            dbContext.BulkInsert(databaseSeeder.SalaryRanges);
            dbContext.BulkInsert(databaseSeeder.Positions);
            dbContext.BulkInsert(databaseSeeder.Employees);
        }
    }
}