using AutoMapper;
using LR_3.DBCommunication.Documents;
using LR_3.DTO.Event;
using LR_3.Models;

namespace LR_3.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventDocument, Event>().ReverseMap();

            CreateMap<CreateEventDTO, Event>();

            CreateMap<UpdateEventDTO, Event>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Event, EventResponseDto>();
        }
    }
}
