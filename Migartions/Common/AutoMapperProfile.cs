using AutoMapper;
using Migartions.Dtos;
using Migartions.Models;

namespace ApiWithEF.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddSportsmanDto, Sportsman>();
            CreateMap<Sportsman, AddSportsmanDto>();

            CreateMap<GetSportsmanDto, Sportsman>();
            CreateMap<Sportsman, GetSportsmanDto>();

            CreateMap<AddEventDto, Event>();
            CreateMap<Event, AddEventDto>();

            CreateMap<GetEventDto, Event>();
            CreateMap<Event, GetEventDto>();

            CreateMap<AddEmployeeDto, Employee>();
            CreateMap<Employee, AddEmployeeDto>();

            CreateMap<GetEmployeeDto, Employee>();
            CreateMap<Employee, GetEmployeeDto>();
        }
    }
}
