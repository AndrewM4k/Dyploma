using AutoMapper;
using SportsCompetition.Dtos;
using SportsCompetition.Models;

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

            CreateMap<AddEmployeeDto, Employee>();
            CreateMap<Employee, AddEmployeeDto>();

            CreateMap<GetEmployeeDto, Employee>();
            CreateMap<Employee, GetEmployeeDto>();
        }
    }
}
