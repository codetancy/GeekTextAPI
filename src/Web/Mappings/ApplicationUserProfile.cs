using AutoMapper;
using Web.Contracts.V1.Responses;
using Web.Data.Identities;

namespace Web.Mappings
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, LoggedUserResponse>(MemberList.Destination);

            CreateMap<ApplicationUser, UserReponse>(MemberList.Destination);
        }
    }
}
