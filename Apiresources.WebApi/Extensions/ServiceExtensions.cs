namespace $safeprojectname$.Extensions
{
    public static class ServiceExtensions
    {
        // Extension method to add Swagger documentation to the service collection
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Clean Architecture - $safeprojectname$",
                    Description = "This Api will be responsible for overall data distribution and authorization.",
                    Contact = new OpenApiContact
                    {
                        Name = "Jane Doe",
                        Email = "jdoe@janedoe.com",
                        Url = new Uri("https://janedoe.com/contact"),
                    }
                });
                // Add security definition for JWT Bearer
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                // Add security requirement to enforce Bearer token use
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }

        // Extension method to add and configure controllers with JSON options
        public static void AddControllersExtension(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Configure JSON serializer to use camelCase
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
        }

        // Extension method to configure CORS with a policy to allow any origin, header, and method
        public static void AddCorsExtension(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }

        // Extension method to add API versioning and configure the API version explorer
        public static void AddVersionedApiExplorerExtension(this IServiceCollection services)
        {
            var apiVersioningBuilder = services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                new HeaderApiVersionReader("x-api-version"),
                                                new MediaTypeApiVersionReader("x-api-version"));
            });
            apiVersioningBuilder.AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        // Extension method to add API versioning for the service
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        // Extension method to set up JWT authentication
        public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = configuration["Sts:ServerUrl"];
                    options.Audience = configuration["Sts:Audience"];
                });
        }

        // Extension method to add authorization policies based on roles
        public static void AddAuthorizationPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            string admin = configuration["ApiRoles:AdminRole"],
                    manager = configuration["ApiRoles:ManagerRole"], employee = configuration["ApiRoles:EmployeeRole"];

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationConsts.AdminPolicy, policy => policy.RequireRole(admin));
                options.AddPolicy(AuthorizationConsts.ManagerPolicy, policy => policy.RequireRole(manager, admin));
                options.AddPolicy(AuthorizationConsts.EmployeePolicy, policy => policy.RequireRole(employee, manager, admin));
            });
        }
    }
}
