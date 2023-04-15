using AutoMapper;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            CreateMap<UpdateSportsmanDto, Sportsman>();
            CreateMap<Sportsman, UpdateSportsmanDto>();

            CreateMap<AddEventDto, Event>();
            CreateMap<Event, AddEventDto>();
            CreateMap<GetEventDto, Event>();
            CreateMap<Event, GetEventDto>();
            CreateMap<UpdateEventDto, Event>();
            CreateMap<Event, UpdateEventDto>();

            CreateMap<AddEmployeeDto, Employee>();
            CreateMap<Employee, AddEmployeeDto>();
                //.ForMember(dest => dest.Role, opt =>opt.MapFrom(src => src.Role.Name));
            CreateMap<GetEmployeeDto, Employee>();
            CreateMap<Employee, GetEmployeeDto>();
                //.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<UpdateEmployeeDto, Employee>();
            CreateMap<Employee, UpdateEmployeeDto>();
                //.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<AddStreamDto, Streama>();
            CreateMap<Streama, AddStreamDto>();
            CreateMap<GetStreamDto, Streama>();
            CreateMap<Streama, GetStreamDto>();
            CreateMap<UpdateStreamDto, Streama>();
            CreateMap<Streama, UpdateStreamDto>();
        }
    }
}
