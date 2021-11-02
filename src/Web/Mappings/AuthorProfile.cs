using System.Linq;
using AutoMapper;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Responses;
using Web.Models;

namespace Web.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<CreateAuthorRequest, Author>(MemberList.Source);

            CreateMap<Author, AuthorResponse>(MemberList.Destination)
                .ForMember(dest => dest.Books,
                    opts => opts.MapFrom(
                        src => src.Books.Select(book => new SimpleBookResponse(book.Id, book.Title))));

            CreateMap<Author, SimpleAuthorResponse>(MemberList.Destination);
        }
    }
}
