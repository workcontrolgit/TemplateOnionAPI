namespace $safeprojectname$.Extensions
{
    // Static class containing extension methods for IApplicationBuilder
    public static class AppExtensions
    {
        // Extension method to configure and use Swagger for API documentation
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            // Enables middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enables middleware to serve Swagger UI at the specified endpoint
            app.UseSwaggerUI(c =>
            {
                // Configures the Swagger endpoint with a name and location of the JSON file
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture.$safeprojectname$");
            });
        }

        // Extension method to add custom middleware for error handling
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            // Uses the ErrorHandlerMiddleware to process exceptions and generate appropriate responses
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
        
    }
}