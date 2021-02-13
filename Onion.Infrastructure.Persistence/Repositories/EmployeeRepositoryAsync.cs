using $ext_projectname$.Application.Features.Employees.Queries.GetEmployees;
using $ext_projectname$.Application.Interfaces;
using $ext_projectname$.Application.Interfaces.Repositories;
using $ext_projectname$.Domain.Entities;
using $safeprojectname$.Contexts;
using $safeprojectname$.Repository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace $safeprojectname$.Repositories
{
    public class EmployeeRepositoryAsync : GenericRepositoryAsync<Employee>, IEmployeeRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Employee> _employee;
        private IDataShapeHelper<Employee> _dataShaper;
        private readonly IMockService _mockData;
        public EmployeeRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Employee> dataShaper,
            IMockService mockData) : base(dbContext)
        {
            _dbContext = dbContext;
            _employee = dbContext.Set<Employee>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }

        public async Task<IEnumerable<Entity>> GetPagedEmployeeReponseAsync(GetEmployeesQuery requestParameter)
        {

            IQueryable<Employee> result;

            var employeeNumber = requestParameter.EmployeeNumber;
            var employeeTitle = requestParameter.EmployeeTitle;
            var orderBy = requestParameter.OrderBy;
            var fields = requestParameter.Fields;
            var pageNumber = requestParameter.PageNumber;
            var pageSize = requestParameter.PageSize;

            // setup IQueryAble
            result = _mockData.GetEmployees(1000)
                .AsQueryable();

            // filter
            FilterByColumn(ref result, employeeNumber, employeeTitle);
            // order by
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                result = result.OrderBy(orderBy);
            }
            // page
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // select columns
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Employee>("new(" + fields + ")");
            }
            // ToList
            // var resultData = await result.ToListAsync();
            // Note: Bogus library does not support await for AsQueryable.
            // Workaround:  fake await with Task.Run and use regular ToList
            var resultData = await Task.Run(() => result.ToList());

            // shape data
            return _dataShaper.ShapeData(resultData, fields);

        }
        private void FilterByColumn(ref IQueryable<Employee> positions, string employeeNumber, string employeeTitle)
        {
            if (!positions.Any())
                return;

            if (string.IsNullOrEmpty(employeeTitle) && string.IsNullOrEmpty(employeeNumber))
                return;

            var predicate = PredicateBuilder.New<Employee>();

            if (!string.IsNullOrEmpty(employeeNumber))
                predicate = predicate.And(p => p.EmployeeNumber.Contains(employeeNumber.Trim()));

            if (!string.IsNullOrEmpty(employeeTitle))
                predicate = predicate.And(p => p.EmployeeTitle.Contains(employeeTitle.Trim()));

            positions = positions.Where(predicate);
        }
    }
}
