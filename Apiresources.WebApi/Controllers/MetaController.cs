namespace $safeprojectname$.Controllers
{
    // MetaController inherits from BaseApiController and handles API requests related to metadata
    public class MetaController : BaseApiController
    {
        // An HTTP GET endpoint at "/info" that returns information about the application
        [HttpGet("/info")]
        public ActionResult<string> Info()
        {
            // Get the assembly information for the current application
            var assembly = typeof(Program).Assembly;

            // Retrieve the last write time of the assembly file, representing the last update time
            var lastUpdate = System.IO.File.GetLastWriteTime(assembly.Location);

            // Retrieve the product version of the assembly
            var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

            // Return an OK response with the version and last updated timestamp of the application
            return Ok($"Version: {version}, Last Updated: {lastUpdate}");
        }
    }
}