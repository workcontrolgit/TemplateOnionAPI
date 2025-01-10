namespace $safeprojectname$.Controllers
{
    // Attribute to indicate that this is an API Controller
    [ApiController]
    // Define the routing for the controller, including API versioning
    [Route("api/v{version:apiVersion}/[controller]")]
    // Define an abstract base class for API controllers that inherit ControllerBase
    public abstract class BaseApiController : ControllerBase
    {
        // Private field to hold the IMediator instance
        private IMediator _mediator;

        // Protected property to lazily initialize the IMediator instance using the service provider
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}