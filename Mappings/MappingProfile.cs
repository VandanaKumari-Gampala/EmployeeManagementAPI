using AutoMapper;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.DTOs;
namespace EmployeeManagementAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<CreateEmployeeDto,Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
        }
    }
}