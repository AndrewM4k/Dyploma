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
            CreateMap<Employee, GetEmployeeDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
            CreateMap<UpdateEmployeeDto, Employee>();
            CreateMap<Employee, UpdateEmployeeDto>();
            //.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<AddStreamDto, SportsCompetition.Models.Stream>();
            CreateMap<SportsCompetition.Models.Stream, AddStreamDto>();
            CreateMap<GetStreamDto, SportsCompetition.Models.Stream>();
            CreateMap<SportsCompetition.Models.Stream, GetStreamDto>();
            CreateMap<UpdateStreamDto, SportsCompetition.Models.Stream>();
            CreateMap<SportsCompetition.Models.Stream, UpdateStreamDto>();

            CreateMap<GetCompetitionDto, Competition>();
            CreateMap<Competition, GetCompetitionDto>();

            CreateMap<GetSportsmanCompetitionDto, SportsmanCompetition>();
            CreateMap<SportsmanCompetition, GetSportsmanCompetitionDto>();

            CreateMap<AddStandartDto, Standart>();
            CreateMap<Standart, AddStandartDto>();
            CreateMap<GetStandartDto, Standart>();
            CreateMap<Standart, GetStandartDto>();
            CreateMap<UpdateStandartDto, Standart>();
            CreateMap<Standart, UpdateStandartDto>();

            CreateMap<AddRecordDto, Record>();
            CreateMap<Record, AddRecordDto>();
            CreateMap<GetRecordDto, Record>();
            CreateMap<Record, GetRecordDto>();
            CreateMap<UpdateRecordDto, Record>();
            CreateMap<Record, UpdateRecordDto>();
        }
    }
}
