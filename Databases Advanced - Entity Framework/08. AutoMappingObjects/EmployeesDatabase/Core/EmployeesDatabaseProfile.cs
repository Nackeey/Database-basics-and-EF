namespace EmployeesDatabase.Core
{
    using AutoMapper;
    using EmployeesDatabase.Core.DTOs;
    using EmployeesDatabase.Models;

    public class EmployeesDatabaseProfile : Profile
    {
        public EmployeesDatabaseProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, ManagerDto>()
                .ForMember(dest => dest.EmployeeDtos, from => from.MapFrom(x => x.ManagerEmployees))
                .ReverseMap();
            CreateMap<Employee, EmployeePersonalInfoDto>().ReverseMap();
        }
    }
}
