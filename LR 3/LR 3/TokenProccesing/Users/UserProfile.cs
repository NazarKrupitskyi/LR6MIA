using AutoMapper;

namespace LR_3.TokenProccesing.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDocument, User>().ReverseMap();

            CreateMap<CreateUserDTO, User>();
            CreateMap<User, UserResponseDTO>();
            CreateMap<UpdateUserDTO, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
