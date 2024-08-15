using $ext_projectname$.Domain.Entities;
using System.Collections.Generic;

namespace $safeprojectname$.Interfaces
{
    public interface IMockService
    {
        List<Position> GetPositions(int rowCount, IEnumerable<Department> departments, IEnumerable<SalaryRange> salaryRanges);

        List<Employee> GetEmployees(int rowCount);

        List<Position> SeedPositions(int rowCount);
    }
}