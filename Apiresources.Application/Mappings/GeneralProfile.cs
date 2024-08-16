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