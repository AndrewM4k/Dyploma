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
            CreateMap<GetEmployeeDto, Employee>();
            CreateMap<Employee, GetEmployeeDto>();
            CreateMap<UpdateEmployeeDto, Employee>();
            CreateMap<Employee, UpdateEmployeeDto>();

            CreateMap<AddStreamDto, Stream>();
            CreateMap<Stream, AddStreamDto>();
            CreateMap<GetStreamDto, Stream>();
            CreateMap<Stream, GetStreamDto>();
            CreateMap<UpdateStreamDto, Stream>();
            CreateMap<Stream, UpdateStreamDto>();
        }
    }
}
