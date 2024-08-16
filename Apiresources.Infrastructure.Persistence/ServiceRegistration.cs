namespace $safeprojectname$
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            #region Repositories

            // * use Scutor to register generic repository interface for DI and specifying the lifetime of dependencies
            services.Scan(selector => selector
                .FromCallingAssembly()
                .AddClasses(classSelector => classSelector.AssignableTo(typeof(IGenericRepositoryAsync<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                );

            #endregion Repositories
        }
    }
}