using $ext_projectname$.Application.Interfaces;
using $ext_projectname$.Domain.Common;
using $ext_projectname$.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Contexts
{
    public partial class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly ILoggerFactory _loggerFactory;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime,
            ILoggerFactory loggerFactory
            ) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _loggerFactory = loggerFactory;
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SalaryRange> SalaryRanges { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring data model using EF Core Fluent
            ApplicationDbContextHelpers.DatabaseModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging().EnableDetailedErrors();
        }
    }
}