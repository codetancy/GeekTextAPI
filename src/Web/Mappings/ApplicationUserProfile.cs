using System.Linq;
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

            CreateMap<ApplicationUser, UserReponse>()
                .ForMember(dest => dest.Roles,
                    opts => opts.MapFrom(
                        src => src.Roles.Select(r => r.Name)));
        }
    }
}
