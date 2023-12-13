using $ext_projectname$.Application.Interfaces;
using $ext_projectname$.Domain.Common;
using $ext_projectname$.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Contexts
{
    public class ApplicationDbContext : DbContext
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

        public DbSet<Position> Positions { get; set; }

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
            // Configure the tables
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            // Mock data
            var _mockData = this.Database.GetService<IMockService>();
            var seedPositions = _mockData.SeedPositions(1000);
            modelBuilder.Entity<Position>().HasData(seedPositions);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
        internal class PositionConfiguration : IEntityTypeConfiguration<Position>
        {
            public void Configure(EntityTypeBuilder<Position> builder)
            {
                builder.ToTable("Positions");
                builder.Property(e => e.Id).ValueGeneratedNever();
                builder.Property(e => e.PositionDescription)
                    .IsRequired()
                    .HasMaxLength(1000);
                builder.Property(e => e.PositionNumber)
                    .IsRequired()
                    .HasMaxLength(100);
                builder.Property(e => e.PositionSalary).HasColumnType("decimal(18, 2)");
                builder.Property(e => e.PositionTitle)
                    .IsRequired()
                    .HasMaxLength(250);
                builder.Property(e => e.PostionType).HasMaxLength(100);
                builder.Property(e => e.PostionArea).HasMaxLength(100);
                builder.Property(e => e.CreatedBy).HasMaxLength(100);
                builder.Property(e => e.LastModifiedBy).HasMaxLength(100);
            }
        }
    }
}