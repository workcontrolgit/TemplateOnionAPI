namespace $safeprojectname$.Contexts
{
    public partial class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime; // Service for getting current date and time
        private readonly ILoggerFactory _loggerFactory; // Factory for creating logger instances

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime, // Injected service for getting current date and time
            ILoggerFactory loggerFactory // Injected factory for creating logger instances
            ) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; // Disable tracking of query results in change tracker
            _dateTime = dateTime;
            _loggerFactory = loggerFactory;
        }

        public DbSet<Department> Departments { get; set; } // Entity set for departments
        public DbSet<Position> Positions { get; set; } // Entity set for positions
        public DbSet<Employee> Employees { get; set; } // Entity set for employees
        public DbSet<SalaryRange> SalaryRanges { get; set; } // Entity set for salary ranges

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added: // If entity is being added to database
                        entry.Entity.Created = _dateTime.NowUtc; // Set created date and time to current UTC time using injected service
                        break;

                    case EntityState.Modified: // If entity is being modified in database
                        entry.Entity.LastModified = _dateTime.NowUtc; // Set last modified date and time to current UTC time using injected service
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken); // Call base method to save changes asynchronously with specified cancellation token
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring data model using EF Core Fluent API, using helper class
            ApplicationDbContextHelpers.DatabaseModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory) // Use specified logger factory for logging operations
                .EnableSensitiveDataLogging() // Enable sensitive data logging for debugging purposes
                .EnableDetailedErrors(); // Enable detailed error messages when an exception occurs during database operation
        }
    }
}