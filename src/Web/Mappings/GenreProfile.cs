using AutoMapper;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Responses;
using Web.Models;

namespace Web.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<CreateGenreRequest, Genre>(MemberList.Source);

            CreateMap<Genre, string>(MemberList.Destination).ConvertUsing(g => g.Name);

            CreateMap<Genre, GenreResponse>(MemberList.Destination);
        }
    }
}
