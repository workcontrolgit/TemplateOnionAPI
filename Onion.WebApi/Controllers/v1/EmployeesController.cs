using $ext_projectname$.Application.Features.Employees.Queries.GetEmployees;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace $safeprojectname$.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EmployeesController : BaseApiController
    {
        /// <summary>
        /// GET: api/controller
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]

        //public async Task<IActionResult> Get([FromQuery] GetAllEmployeesParameter filter)
        public async Task<IActionResult> Get([FromQuery] GetEmployeesQuery filter)
        {
            //return Ok(await Mediator.Send(new GetAllEmployeesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
            return Ok(await Mediator.Send(filter));
        }

    }
}
