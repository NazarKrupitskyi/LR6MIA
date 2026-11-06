using AutoMapper;
using LR_3.DBCommunication.Documents;
using LR_3.DTO.TicketOrder;
using LR_3.DTO.TicketTier;
using LR_3.Models;

namespace LR_3.Profiles
{
    public class TicketTierProfile : Profile
    {
        public TicketTierProfile()
        {
            CreateMap<TicketTierDocument, TicketTier>().ReverseMap();

            CreateMap<CreateTicketTierDTO, TicketTier>();
            CreateMap<UpdateTicketTierDTO, TicketTier>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<TicketTier, TicketTierResponseDTO>();
        }
    }
}
    