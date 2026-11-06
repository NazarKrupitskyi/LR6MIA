using AutoMapper;
using LR_3.DBCommunication.Documents;
using LR_3.DTO.TicketOrder;
using LR_3.Models;

namespace LR_3.Profiles
{
    public class TicketOrderProfile : Profile
    {
        public TicketOrderProfile()
        {
            CreateMap<TicketOrderDocument, TicketOrder>().ReverseMap();

            CreateMap<CreateTicketOrderDTO, TicketOrder>();

            CreateMap<UpdateTicketOrderDTO, TicketOrder>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<TicketOrder, TicketOrderResponseDTO>();
            CreateMap<DigitalTicket, DigitalTicketResponseDto>();
        }
    }
}
