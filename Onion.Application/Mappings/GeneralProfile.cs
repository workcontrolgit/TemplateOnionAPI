using AutoMapper;
using $safeprojectname$.Features.Employees.Queries.GetEmployees;
using $safeprojectname$.Features.Positions.Commands.CreatePosition;
using $safeprojectname$.Features.Positions.Queries.GetPositions;
using $ext_projectname$.Domain.Entities;

namespace $safeprojectname$.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Position, GetPositionsViewModel>().ReverseMap();
            CreateMap<Employee, GetEmployeesViewModel>().ReverseMap();
            CreateMap<CreatePositionCommand, Position>();
        }
    }
    
}
