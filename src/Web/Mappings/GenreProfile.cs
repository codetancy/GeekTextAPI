using System.Collections.Generic;
using System.Linq;
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

            CreateMap<List<Genre>, GenreNamesResponse>(MemberList.Destination)
                .ForMember(dest => dest.Genres,
                    opts => opts.MapFrom(
                        src => src.Select(g => g.Name)));

            CreateMap<Genre, GenreResponse>(MemberList.Destination);
        }
    }
}
