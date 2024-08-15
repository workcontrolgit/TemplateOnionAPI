using $safeprojectname$.Features.Departments.Queries.GetDepartments;
using $safeprojectname$.Features.Employees.Queries.GetEmployees;
using $safeprojectname$.Features.Positions.Commands.CreatePosition;
using $safeprojectname$.Features.Positions.Queries.GetPositions;
using $safeprojectname$.Features.SalaryRanges.Queries.GetSalaryRanges;
using $ext_projectname$.Domain.Entities;
using AutoMapper;

namespace $safeprojectname$.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Position, GetPositionsViewModel>().ReverseMap();
            CreateMap<Employee, GetEmployeesViewModel>().ReverseMap();
            CreateMap<Department, GetDepartmentsViewModel>().ReverseMap();
            CreateMap<SalaryRange, GetSalaryRangesViewModel>().ReverseMap();
            CreateMap<CreatePositionCommand, Position>();
        }
    }
}