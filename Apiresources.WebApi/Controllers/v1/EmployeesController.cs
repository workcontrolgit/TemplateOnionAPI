namespace $safeprojectname$.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EmployeesController : BaseApiController
    {

        /// <summary>
        /// Gets a list of employees based on the specified filter.
        /// </summary>
        /// <param name="filter">The filter used to get the list of employees.</param>
        /// <returns>A list of employees.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetEmployeesQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        /// <summary>
        /// Retrieves a paged list of employees.
        /// Support Ngx-DataTables https://medium.com/scrum-and-coke/angular-11-pagination-of-zillion-rows-45d8533538c0
        /// </summary>
        /// <param name="query">The query parameters for the paged list.</param>
        /// <returns>A paged list of employees.</returns>
        [HttpPost]
        [Route("Paged")]
        public async Task<IActionResult> Paged(PagedEmployeesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

    }
}