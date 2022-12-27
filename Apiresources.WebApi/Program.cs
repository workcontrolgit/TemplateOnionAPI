using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using $ext_projectname$.Application;
using $ext_projectname$.Infrastructure.Persistence;
using $ext_projectname$.Infrastructure.Persistence.Contexts;
using $ext_projectname$.Infrastructure.Shared;
using $ext_projectname$.WebApi.Extensions;
using Serilog;
using System;

try
{
    var builder = WebApplication.CreateBuilder(args);
    // load up serilog configuraton
    Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
    builder.Host.UseSerilog(Log.Logger);
    builder.Services.AddApplicationLayer();
    builder.Services.AddPersistenceInfrastructure(builder.Configuration);
    builder.Services.AddSharedInfrastructure(builder.Configuration);
    builder.Services.AddSwaggerExtension();
    builder.Services.AddControllersExtension();
    // CORS
    builder.Services.AddCorsExtension();
    builder.Services.AddHealthChecks();
    //API Security
    builder.Services.AddJWTAuthentication(builder.Configuration);
    builder.Services.AddAuthorizationPolicies(builder.Configuration);
    // API version
    builder.Services.AddApiVersioningExtension();
    // API explorer
    builder.Services.AddMvcCore()
        .AddApiExplorer();
    // API explorer version
    builder.Services.AddVersionedApiExplorerExtension();
    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // use context
        dbContext.Database.EnsureCreated();
    }


    // Add this line; you'll need `using Serilog;` up the top, too
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseRouting();
    //Enable CORS
    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSwaggerExtension();
    app.UseErrorHandlingMiddleware();
    app.UseHealthChecks("/health");
    app.MapControllers();
    app.Run();

<<<<<<< HEAD
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration)) //Uses Serilog instead of default .NET Logger
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
=======
    Log.Information("Application Starting");

}
catch (Exception ex)
{
    Log.Warning(ex, "An error occurred starting the application");
}
finally
{
    Log.CloseAndFlush();
>>>>>>> release/7.1.0
}
